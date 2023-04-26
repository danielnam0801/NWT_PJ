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
            collision.gameObject.GetComponent<PlayerMovement>().SetVerticalVelocity(_jumpPower);
        }
    }
}
