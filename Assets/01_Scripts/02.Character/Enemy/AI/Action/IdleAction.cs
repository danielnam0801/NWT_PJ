using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void InitAction()
    {
        _aiActionData.IsIdle = true;
        _aiMovementData.pointOfInterest = transform.position;
        _aiMovementData.Speed = _brain.Enemy.EnemyData.GetBeforeSpeed;
        _animator.SetAnimatorSpeed(1);
    }

    public override void TakeAction()
    {
        Debug.Log("IDle");
        if(_aiMovementData.direction.x == 0 && _aiMovementData.direction.y == 0)
        {
            _aiMovementData.Speed = 0;
        }
        else
        {
            _aiMovementData.Speed = _brain.Enemy.EnemyData.GetBeforeSpeed;
        }

        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);

    }

    public override void ExitAction()
    {

    }
}
