using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void InitAction()
    {
        
    }

    public override void TakeAction()
    {
        _aiActionData.isIdle = true;
        _aiMovementData.pointOfInterest = transform.position;
        if(_aiMovementData.direction.x == 0 && _aiMovementData.direction.y == 0)
        {
            _aiMovementData.speed = 0;
        }
        else
        {
            _aiMovementData.speed = _brain.Enemy.EnemyData.GetBeforeSpeed;
        }

        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);

    }

    public override void ExitAction()
    {

    }
}
