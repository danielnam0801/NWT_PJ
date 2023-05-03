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
        
        //�� ���� �ִϸ��̼� ������������
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
