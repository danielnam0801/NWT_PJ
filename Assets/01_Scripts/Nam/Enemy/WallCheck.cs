using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    AIMovementData _movement;
    [SerializeField] LayerMask _wallLayer;

    private void Start()
    {
        _movement = transform.Find("AI").GetComponent<AIMovementData>();
    }

    private void Update()
    {
        RaycastHit2D sideWalkCheck = Physics2D.Raycast(transform.position, new Vector3(_movement.direction.x, 0, 0), 1.5f, _wallLayer);
        Debug.DrawRay(transform.position, new Vector3(_movement.direction.x, 0, 0) * 1.5f, Color.black);
        if (sideWalkCheck.collider != null)
        {
            _movement.direction.x = -_movement.direction.x;
        }
    }
}
