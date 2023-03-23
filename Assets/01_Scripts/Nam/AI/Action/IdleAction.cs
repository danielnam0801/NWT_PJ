using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    [SerializeField] float thinkTime = 4f;
    float t = 0;

    public override void TakeAction()
    {
        Debug.Log("IDleAction");
        //_aiActionData.isIdle = true;
        //_aiMovementData.pointOfInterest = transform.position;
        ////_aiMovementData.speed = _brain.Enemy.EnemyData.BeforeDetectSpeed();
            
        //_brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }
}
