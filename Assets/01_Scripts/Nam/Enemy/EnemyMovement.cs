using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    Enemy _enemy;

    private Rigidbody2D rb;

    private int nextMove = 0;

    public UnityEvent<float> onVelocityChange;

    protected float _currentVelocity = 0;
    protected Vector2 _movementdirection;

    AIMovementData _data;

    protected virtual void Awake()
    {
        _enemy = transform.GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        _data = transform.Find("AI").GetComponent<AIMovementData>();
    }

    public void MoveAgent(Vector2 moveInput)
    {
        _currentVelocity = _data.speed;
        _movementdirection = moveInput;
    }


    private void FixedUpdate()
    {
        onVelocityChange?.Invoke(_movementdirection.x);
        rb.velocity = new Vector2(_movementdirection.x * _currentVelocity, rb.velocity.y);
    }

    public void StopImmediatelly()
    {
        _currentVelocity = 0;
        rb.velocity = Vector2.zero;
    }
}
