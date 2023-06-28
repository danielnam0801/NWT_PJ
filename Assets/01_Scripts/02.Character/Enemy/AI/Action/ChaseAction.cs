using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void InitAction()
    {

    }

    public override void TakeAction()
    {
        Vector2 dir = (_brain.Target.position.x - transform.position.x > 0) ? new Vector2(1, 0) : new Vector2(-1, 0);
        _aiActionData.IsIdle = false;
        _aiMovementData.direction = dir;
        _aiMovementData.pointOfInterest = _brain.Target.position;
        _aiMovementData.speed = _brain.Enemy.EnemyData.GetAfterSpeed;

        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }

    public override void ExitAction()
    {

    }

}
