using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : EnemyMovement
{
    private void FixedUpdate()
    {
        if (_isknockBack == true)
        {
            if (CalculateKnockBack())
            {
                rb.velocity = _movementdirection * _currentVelocity;
            }
            else
            {
                _isknockBack = false;
                _brain.UseBrain = true;
                _brain.AIMovementData.canMove = true;
            }
        }
        else
        {
            rb.velocity = new Vector2(_movementdirection.x * _currentVelocity, rb.velocity.y);
        }
    }
}
