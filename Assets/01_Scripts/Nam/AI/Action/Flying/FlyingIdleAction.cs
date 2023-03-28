using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingIdleAction : AIAction
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

    public override void TakeAction()
    {
        _aiActionData.isIdle = true;
        _aiMovementData.pointOfInterest = transform.position;
        _aiMovementData.speed = _brain.Enemy.EnemyData.GetBeforeSpeed;

        float x = Time.time;
        float value = Wave(a1, f1, p1, x) + Wave(a2, f2, p2, x) + Wave(a3, f3, p3, x);
        Debug.Log("Value : " + value);
        _aiMovementData.direction.y = value / 4;
        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }

    float Wave(float a, float f, float p, float x) => a * Mathf.Sin(f* x + p) + y_Axis;
}
