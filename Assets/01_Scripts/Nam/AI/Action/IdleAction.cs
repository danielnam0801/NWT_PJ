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
        _aiActionData.isIdle = true;
        _aiMovementData.pointOfInterest = transform.position;
        _aiMovementData.speed = _brain.Enemy.EnemyDataSO.GetBeforeSpeed;
        if (_aiActionData.isCanThinking)
        {
            _aiMovementData.direction = new Vector2(NextMoveX(), _aiMovementData.direction.y);
        }
    }

    private int NextMoveX()
    {
        int rand = UnityEngine.Random.Range(-1, 2);

        switch (rand)
        {
            case -1:
                break;
            case 0:
                break;
            case 1:
                break;
        }

        return rand;
    }
}
