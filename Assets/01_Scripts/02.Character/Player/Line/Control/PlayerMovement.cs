using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask layer;
    public bool isGround = false;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;

    private float verticalVelocity;
    private Vector2 moveDir;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
            isGround = true;
    }

    public void Move(Vector2 dir)
    {
        Vector2 moveVelocity;

        moveDir = dir;
        moveVelocity = new Vector2(moveDir.x * moveSpeed, rb.velocity.y);

        rb.velocity = moveVelocity;
    }

    public void StopImmediately()
    {
        rb.velocity = Vector2.zero;
    }

    public void Jump()
    {   
        if(isGround)
        {
            rb.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);
            isGround = false;
        }
    }
}
