using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovement : EnemyMovement
{
    private void FixedUpdate()
    {
        if (!_data.canMove) return;

        rb.velocity = new Vector2(_movementdirection.x, _movementdirection.y) * _currentVelocity;
    }
}
