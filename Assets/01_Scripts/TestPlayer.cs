using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] private float _playerSpeed;
    [SerializeField] float d;
    private Rigidbody2D _rigid;
    private Animator _animator;

    public bool hit;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();           
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        RaycastHit2D isGround = Physics2D.Raycast(transform.position, Vector2.down, d, layer);

        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, 5);
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        _rigid.velocity = new Vector2(x * _playerSpeed, _rigid.velocity.y);

        _animator.SetFloat("Move", x);
    }
}
