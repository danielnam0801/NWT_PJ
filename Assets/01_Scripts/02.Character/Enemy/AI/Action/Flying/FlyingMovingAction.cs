using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovingAction : AIAction
{
    [Header("Wave1")]
    [Range(-5f, 5f)] public float a1 = 3;
    [Range(-5f, 5f)] public float f1 = 1;
    [Range(-5f, 5f)] public float p1 = 0;

    [Header("Wave 2")]
    [Range(-5f, 5f)] public float a2 = 2;
    [Range(-5f, 5f)] public float f2 = 3;
    [Range(-5f, 5f)] public float p2 = 0;

    [Header("Wave 3")]
    [Range(-5f, 5f)] public float a3 = 1;
    [Range(-5f, 5f)] public float f3 = 4;
    [Range(-5f, 5f)] public float p3 = 0;

    [Header("Another..")]
    [Range(-5f, 5f)] public float y_Axis = 0;

    [Space(30f)]
    public float randA, randF, randP, randY;

    public override void InitAction()
    {
        _aiMovementData.direction.x = Random.Range(-3f, 3f);
    }

    public override void TakeAction()
    {
        float x = Time.time;
        float value = Wave(a1, f1, p1, x) + Wave(a2, f2, p2, x) + Wave(a3, f3, p3, x);

        if (_aiActionData.isCanThinking)
        {
            StartCoroutine(nameof(Think));
            _aiActionData.isCanThinking = false;
            _aiMovementData.direction.x = value;
        }

        _aiMovementData.direction.y = value / 4; //속도 줄이려 나눈거
        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }

    public override void ExitAction()
    {

    }

    float Wave(float a, float f, float p, float x) => (a + randA) * Mathf.Sin((f + randF) * x + p + randP) + y_Axis + randY;
    IEnumerator Think()
    {
        randA = UnityEngine.Random.Range(-0.5f, 0.5f);
        randF = UnityEngine.Random.Range(-0.5f, 0.5f);
        randP = UnityEngine.Random.Range(-0.5f, 0.5f);
        randY = UnityEngine.Random.Range(-0.5f, 0.5f);

        yield return new WaitForSeconds(_aiMovementData.thinkTime);
        _aiActionData.isCanThinking = true;
        _aiMovementData.beforeDirection = new Vector2(_aiMovementData.direction.x, _aiMovementData.direction.y);
    }
}