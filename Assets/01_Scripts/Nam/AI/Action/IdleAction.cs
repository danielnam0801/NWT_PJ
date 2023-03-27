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
            _aiActionData.isCanThinking = false;
            _aiMovementData.direction = new Vector2(NextMoveX(), _aiMovementData.direction.y);
            _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
        }
    }

    private int NextMoveX()
    {
        int rand = UnityEngine.Random.Range(-1, 2);
        float waitTime = 0f;
        switch (rand)
        {
            case -1:
                waitTime = UnityEngine.Random.Range(thinkTime, thinkTime + 1f);
                break;
            case 0:
                waitTime = UnityEngine.Random.Range(thinkTime-2f, thinkTime);
                break;
            case 1:
                waitTime = UnityEngine.Random.Range(thinkTime, thinkTime + 1f);
                break;
        }

        StartCoroutine(ThinkWait(waitTime));
        return rand;
    }

    IEnumerator ThinkWait(float time)
    {
        yield return new WaitForSeconds(time);
        _aiActionData.isCanThinking = true;
    }
}
