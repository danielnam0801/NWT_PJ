using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveVector;
    private float verticalVelocity;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetMoveVector(Vector2 dir, float speed)
    {
        moveVector = dir.normalized * speed;
    }

    private void Move()
    {
        Vector2 move = moveVector + rigid.velocity.y * Vector2.up;
        rigid.velocity = move;
    }
}
