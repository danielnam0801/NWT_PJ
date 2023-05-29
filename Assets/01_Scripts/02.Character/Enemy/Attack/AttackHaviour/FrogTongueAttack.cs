using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrogTongueAttack : EnemyAttack, IRangeAttack
{
    [SerializeField] float attackRange;
    [SerializeField] Vector2 attackSize;
    [SerializeField] float damage = 1;

    public void Attack(Action CallBack)
    {
        SetAnimAttack();
        AttackStartFeedback?.Invoke();
        _animator.OnAnimaitionEndTrigger += DamageCaster;
        
        //혀 공격 애니메이션 실행시켜줘야함
        callBack = CallBack;
    }

    public void DamageCaster()
    {
        
        StartCoroutine(DelayCoroutine(0.1f, () =>
        {
            Debug.Log("ISATTackINs");
            AttackEndFeedback?.Invoke();
            callBack?.Invoke();
            _animator.OnAnimaitionEndTrigger -= DamageCaster;
        }));
    }
}
