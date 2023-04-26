using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SuperJumpObstacle : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField] LayerMask _layer;
    [SerializeField] private float _jumpPower;
    [SerializeField] private ParticleSystem _particlePrefab;

    private void Awake()
    {
        _rigid = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        { 
            _rigid.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

            PoolManager.Instance.Pop(_particlePrefab.name);
        }
    }
}
