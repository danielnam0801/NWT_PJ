using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 groundCheckPos;
    [SerializeField]
    private Vector2 groundCheckSize;
    [SerializeField]
    private LayerMask groundLayer;

    private Vector2 moveVector;
    private float verticalVelocity = 0;
    private float gravityScale = -9.81f * 2;

    private Rigidbody2D rigid;
    public Vector2 Velocity { get => rigid.velocity + verticalVelocity * Vector2.up; }
    public float GravityScale { get => gravityScale; }

    private bool applyGravity = true;
    public bool ApplyGravity
    {
        get => applyGravity;

        set
        {
            applyGravity = value;
            if (value == false)
                SetVerticalVelocity(0);
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Gravity();
        Move();
    }

    public void SetMoveVector(Vector2 dir, float speed)
    {
        moveVector = dir.normalized * speed;

        if (moveVector.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (moveVector.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetVerticalVelocity(float value)
    {
        verticalVelocity = value;
    }

    public bool CheckGround()
    {
        return Physics2D.OverlapBox(groundCheckPos + (Vector2)transform.position, groundCheckSize, 0, groundLayer);
    }

    private void Move()
    {
        Vector2 move = moveVector + verticalVelocity * Vector2.up;
        rigid.velocity = move;
    }

    private void Gravity()
    {
        if (!applyGravity)
            return;

        if (!CheckGround())
            verticalVelocity += gravityScale * Time.fixedDeltaTime;
        else
        {
            if(verticalVelocity < 0)
            {
                verticalVelocity = 0;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos + (Vector2)transform.position, groundCheckSize);
    }
#endif
}
