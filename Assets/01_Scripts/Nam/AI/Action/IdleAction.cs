using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{

    public override void TakeAction()
    {
        _aiActionData.isIdle = true;
        _aiMovementData.pointOfInterest = transform.position;
        _aiMovementData.speed = _brain.Enemy.EnemyData.GetBeforeSpeed;

        if (_aiActionData.isCanThinking)
        {
            Debug.Log("EnemyThink " + _brain.gameObject.name);
            _aiMovementData.direction = new Vector2(NextMove(), 0);
            _aiActionData.isCanThinking = false;
        }
        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);

    }

    public int NextMove()
    {

        int nextMove = UnityEngine.Random.Range(-1, 2);

        Debug.Log("NextMove : " + nextMove);

        _aiMovementData.thinkTime = UnityEngine.Random.Range(4.5f, 5.5f);

        if (nextMove == 0) _aiMovementData.thinkTime -= UnityEngine.Random.Range(3f, 3.5f);

        StartCoroutine("EnemyThink", _aiMovementData.thinkTime);
        return nextMove;
    }

    IEnumerator EnemyThink(float thinkTime)
    {
        yield return new WaitForSeconds(5f);
        _aiActionData.isCanThinking = true;
        _aiMovementData.beforeDirection = new Vector2(_aiMovementData.direction.x, _aiMovementData.direction.y);
    }
}
