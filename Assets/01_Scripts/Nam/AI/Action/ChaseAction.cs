using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Debug.Log("Chase");
        if (_aiActionData.isAttack)
        {
            _aiActionData.isAttack = false;
        }

        Vector2 dir = (_brain.Target.position.x - transform.position.x > 0) ? new Vector2(1, 0) : new Vector2(-1, 0);
        Debug.Log(dir);
        _aiActionData.isIdle = false;
        _aiMovementData.direction = dir;
        _aiMovementData.pointOfInterest = _brain.Target.position;
        _aiMovementData.speed = _brain.Enemy.EnemyData.GetAfterSpeed;

        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }

}
