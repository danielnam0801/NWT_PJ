using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void Init()
    {

    }

    public override void TakeAction()
    {
        _aiActionData.isIdle = true;
        _aiMovementData.pointOfInterest = transform.position;
        _aiMovementData.speed = _brain.Enemy.EnemyData.GetBeforeSpeed;

        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);

    }
}
