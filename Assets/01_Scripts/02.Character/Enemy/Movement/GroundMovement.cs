using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : EnemyMovement
{
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_movementdirection.x * _currentVelocity, rb.velocity.y);
    }
}
