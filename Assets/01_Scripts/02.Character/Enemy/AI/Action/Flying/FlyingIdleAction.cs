using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingIdleAction : AIAction
{
    public override void InitAction()
    {
        _aiActionData.IsIdle = true;
        _aiMovementData.pointOfInterest = transform.position;
        _aiMovementData.Speed = _brain.Enemy.EnemyData.GetBeforeSpeed;
    }

    public override void TakeAction()
    {
        //_brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);// flying movingø° ¿÷¿Ω
    }

    public override void ExitAction()
    {

    }
}
