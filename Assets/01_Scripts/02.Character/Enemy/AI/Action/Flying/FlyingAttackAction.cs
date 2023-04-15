using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackAction : AIAction
{
    public SkillName skillName;
    public override void InitAction()
    {
        
    }


    public override void TakeAction()
    {
        //_aiMovementData.direction = Vector2.zero;
        _aiActionData.isIdle = false;
        _brain.Attack(skillName);
        _aiMovementData.pointOfInterest = _brain.Target.position;
    }

    public override void ExitAction()
    {

    }
}
