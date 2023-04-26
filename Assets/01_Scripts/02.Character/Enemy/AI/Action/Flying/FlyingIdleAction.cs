using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingIdleAction : AIAction
{
    public override void InitAction()
    {
        Debug.Log(gameObject.name + "changeState");
    }

    public override void TakeAction()
    {
        _aiActionData.isIdle = true;
        _aiMovementData.pointOfInterest = transform.position;
        _aiMovementData.speed = _brain.Enemy.EnemyData.GetBeforeSpeed;

        //_brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);// flying movingø° ¿÷¿Ω
    }

    public override void ExitAction()
    {

    }
}
