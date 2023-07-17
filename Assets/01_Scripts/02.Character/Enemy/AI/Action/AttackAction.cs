using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAction : AIAction
{
    public override void InitAction()
    {
        _animator.SetAttackState(true);
        _animator.SetAttackTrigger(_aiActionData.nextSkill);
    }
    public override void TakeAction() { }
    public override void ExitAction()
    {
        _stateInfo.Init();
        _animator.SetAttackState(false);
    }
}