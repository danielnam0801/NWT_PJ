using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerState
{
    [SerializeField]
    private float moveSpeed;

    public override void EnterState()
    {
        input.OnMovementInput += Move;
    }

    public override void ExitState()
    {
        input.OnMovementInput -= Move;
    }

    public override void UpdateState()
    {
        
    }

    private void Move(Vector2 inputVector)
    {
        movement?.SetMoveVector(inputVector, moveSpeed);
    }
}
