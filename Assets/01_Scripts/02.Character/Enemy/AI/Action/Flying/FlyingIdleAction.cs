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
        
        int dirX = Random.Range(-1, 1);
        if(dirX == 0) dirX = 1;
        _aiMovementData.direction.x = dirX;
    }

    public override void TakeAction()
    {
        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);// flying movingø° ¿÷¿Ω
    }

    public override void ExitAction()
    {
        _aiMovementData.beforeDirection = new Vector2(_aiMovementData.direction.x, _aiMovementData.direction.y);
    }
}
