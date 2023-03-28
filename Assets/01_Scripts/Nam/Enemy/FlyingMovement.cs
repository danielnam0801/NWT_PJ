using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovement : EnemyMovement
{
    private void FixedUpdate()
    {
        onVelocityChange?.Invoke(_movementdirection.x);
        rb.velocity = new Vector2(_movementdirection.x * _currentVelocity, _movementdirection.y);
    }
}
