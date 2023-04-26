using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SuperJumpObstacle : MonoBehaviour
{
    [SerializeField] LayerMask _layer;
    [SerializeField] private float _jumpPower;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.GetComponent<Rigidbody2D>());

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
}
