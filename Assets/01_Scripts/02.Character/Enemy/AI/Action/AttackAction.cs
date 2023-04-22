using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAction : AIAction
{
    public SkillName skillName;
    public UnityEvent AttackFeedback;
    public UnityEvent EndEvent;

    public override void InitAction()
    {
        _animator.SetSpeed(0);
        _animator.OnAnimaitionEventTrigger += AttackFeedbackPlay;
        _animator.OnAnimaitionEndTrigger += SetEnd;
        SetAnimAttack();
    }


    public override void TakeAction()
    {
        _brain.Attack(skillName);
    }

    public override void ExitAction()
    {
        _animator.OnAnimaitionEndTrigger -= SetEnd;
        _animator.OnAnimaitionEventTrigger -= AttackFeedbackPlay;
    }

    void AttackFeedbackPlay()
    {
        AttackFeedback?.Invoke();
    }

    void SetAnimAttack()
    {
        _animator.SetAttackTrigger(true, skillName);
    }

    void SetEnd()
    {
        _animator.SetAttackTrigger(false, skillName);
        EndEvent?.Invoke();
    }
}
