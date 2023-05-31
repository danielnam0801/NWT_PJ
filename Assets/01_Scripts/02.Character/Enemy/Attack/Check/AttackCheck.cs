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
            case AttackCheckType.CIRCLE_COLLISION:
                circle = true;
                collisionCheck = true;
                break;
            case AttackCheckType.BOX_COLLISION:
                box = true;
                collisionCheck = true;
                break;
            case AttackCheckType.CIRCLE_COLLIDER:
                circle = true;
                colliderCheck = true;
                break;
            case AttackCheckType.BOX_COLLIDER:
                box = true;
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
    
}