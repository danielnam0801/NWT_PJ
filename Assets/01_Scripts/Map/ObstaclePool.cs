using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : PoolableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _time;
    [SerializeField] private Vector2 _dir;

    private Rigidbody2D _rigid;

    public override void Init()
    {
    }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveObstacle();
        Invoke("PoolPush", _time);
    }

    private void MoveObstacle()
    {
        _rigid.velocity = _dir * _speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PoolPush();
        }
    }
    
    private void PoolPush()
    {
        PoolManager.Instance.Push(this);
    }
}
