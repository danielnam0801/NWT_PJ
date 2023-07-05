using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingChaseAction : AIAction
{
    public override void InitAction()
    {
        _aiMovementData.Speed = _brain.Enemy.EnemyData.GetAfterSpeed;        
        _aiMovementData.pointOfInterest = _brain.Target.position;
    }

    public override void TakeAction()
    {
        int dirX = (_brain.Target.position.x - transform.position.x > 0) ? 1 : -1;
        int dirY = (_brain.Target.position.y - transform.position.y > 0) ? 1 : -1;
        _aiMovementData.direction = new Vector2(
            dirX,
            Mathf.Abs(_aiMovementData.direction.y) * dirY
        );

        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }

    public override void ExitAction()
    {
        
    }

}
