using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrogTongueAttack : EnemyAttack
{
    [SerializeField] float attackRange;
    [SerializeField] Vector2 attackSize;
    [SerializeField] float damage = 1;
    

    public override void Attack(Action CallBack)
    {
        AttackStartFeedback?.Invoke();
        _animator.OnAnimaitionEndTrigger += DamageCaster;
        
        //혀 공격 애니메이션 실행시켜줘야함
        callBack = CallBack;
    }

    public void DamageCaster()
    {
        RaycastHit2D damageCast = Physics2D.BoxCast(_brain.Enemy.RayPoint.position, attackSize, 0, new Vector2(_brain.AIMovementData.direction.x,0),attackRange, CanDamageble);
        if (damageCast.collider != null)
        {
            damageCast.collider.GetComponent<IHitable>().GetHit(damage, this.gameObject);
            Debug.Log("IsHIt");
        }
        
        StartCoroutine(DelayCoroutine(0.1f, () =>
        {
            Debug.Log("ISATTackINs");
            AttackEndFeedback?.Invoke();
            callBack?.Invoke();
            _animator.OnAnimaitionEndTrigger -= DamageCaster;
        }));
    }
}
