using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBeeMovement : FlyingMovement
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

    [SerializeField] float ySpeed = 0.5f;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (_isknockBack == true) return;

        float x = Time.time;
        float value = Wave(a1, f1, p1, x) + Wave(a2, f2, p2, x) + Wave(a3, f3, p3, x);

        MovementDirection = new Vector2(MovementDirection.x, value * ySpeed).normalized;
        _data.direction = MovementDirection;

        _brain.Move(MovementDirection, transform.position);
    }

    float Wave(float a, float f, float p, float x) => a * Mathf.Sin(f * x + p);
}