using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAction : AIAction
{
    public SkillName skillName;
    public UnityEvent EndEvent;

    protected AttackCoolController attackCoolController;

    protected override void Awake()
    {
        base.Awake();
        attackCoolController = _brain.transform.GetComponent<AttackCoolController>();
    }

    public override void InitAction()
    {
        _animator.SetSpeed(0);
        SetAnimAttack();
    }


    public override void TakeAction()
    {
        _brain.Attack(skillName);
    }

    public override void ExitAction()
    {

    }

    void SetAnimAttack()
    {
        _animator.SetAttackTrigger(true, skillName);
    }
}
