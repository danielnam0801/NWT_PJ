using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        _aiActionData.isIdle = false;
        _aiActionData.isCanThinking = true;
        _aiMovementData.pointOfInterest = _brain.Target.position;
        _aiMovementData.speed = _brain.Enemy.EnemyDataSO.GetAfterSpeed;

        float dir = (transform.position - _brain.Target.position).normalized.x;
        _aiMovementData.direction = new Vector2(dir,transform.position.y);

        _brain.Move(_aiMovementData.direction, _brain.Target.position);
    }
}
