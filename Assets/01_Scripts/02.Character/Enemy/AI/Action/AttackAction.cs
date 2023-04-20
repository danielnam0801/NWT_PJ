using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public SkillName skillName;

    public override void InitAction()
    {
        _animator.SetSpeed(0);
        _animator.OnAnimaitionEndTrigger += SetEnd;
        SetAnimAttack();
    }


    public override void TakeAction()
    {
        _brain.Attack(skillName);
    }

    public override void ExitAction()
    {
        Debug.Log("EndAttack");
        _animator.OnAnimaitionEndTrigger -= SetEnd;
    }
    void SetAnimAttack()
    {
        _animator.SetAttackTrigger(true, skillName);
    }

    void SetEnd()
    {
        _animator.SetAttackTrigger(false, skillName);
    }
}
