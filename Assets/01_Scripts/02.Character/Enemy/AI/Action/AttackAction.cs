using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public SkillName skillName;

    private bool firstIn = false;
    public override void InitAction()
    {
        firstIn = false;
        _animator.SetSpeed(0);
        _animator.OnAnimaitionEndTrigger += SetEnd;
    }


    public override void TakeAction()
    {
        if (!firstIn)
        {
            firstIn = true;
            DelayCoroutine(0.2f,SetAnimAttack);
        }
        _brain.Attack(skillName);
    }

    public override void ExitAction()
    {
        _animator.OnAnimaitionEndTrigger -= SetEnd;
    }
    void SetAnimAttack()
    {
        _animator.SetAttackState(true);
        _animator.SetAttackTrigger(true, skillName);
    }

    void SetEnd()
    {
        _animator.SetAttackState(false);
        _animator.SetAttackTrigger(false, skillName);
        _stateInfo.IsAttack = false;
    }
}
