using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] private AttackCheckType _attackCheckType;
    [SerializeField] float damage = 5f;

    private bool collisionCheck;
    private bool colliderCheck;
    private bool rayCheck;
    private bool overlapCheck;

    private bool circle;
    private bool box;
    private bool line;
    private void Awake()
    {
        switch (_attackCheckType)
        {
            case AttackCheckType.COLLISION:
                collisionCheck = true;
                break;
            case AttackCheckType.COLLIDER:
                colliderCheck = true;
                break;
            case AttackCheckType.CIRCLE_OVERLAP:
                circle = true;
                overlapCheck = true;
                break;
            case AttackCheckType.BOX_OVERLAP:
                box = true;
                overlapCheck = true;
                break;
            case AttackCheckType.LINE_RAYCAST:
                line = true;
                rayCheck = true;
                break;
            case AttackCheckType.BOX_RAYCAST:
                box = true;
                rayCheck = true;
                break;
            case AttackCheckType.CIRCLE_RAYCAST:
                circle = true;
                rayCheck = true;
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionCheck)
        {
            IHitable hitable;
            if(collision.gameObject.TryGetComponent<IHitable>(out hitable)){
                hitable.GetHit(damage, this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colliderCheck)
        {
            IHitable hitable;
            if (collision.gameObject.TryGetComponent<IHitable>(out hitable))
            {
                hitable.GetHit(damage, this.gameObject);
            }
        }
    }

    private void Update()
    {
        
    }

    public void OverlapCheck()
    {

    }

    public void RayCheck()
    {

    }



}