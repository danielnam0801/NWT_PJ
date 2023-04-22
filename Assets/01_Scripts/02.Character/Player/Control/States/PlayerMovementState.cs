using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerState
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private int maxJumpCount;
    private int currentJumpCount = 0;


    private float gravityScale = -9.81f;

    public override void EnterState()
    {
        input.OnMovementInput += Move;
        input.OnSpaceInput += Jump;
        input.OnShiftInput += DashHandle;
    }

    public override void ExitState()
    {
        input.OnMovementInput -= Move;
        input.OnSpaceInput -= Jump;
        input.OnShiftInput -= DashHandle;
        movement?.SetMoveVector(Vector2.zero, 0);
    }

    public override void UpdateState()
    {
        if (movement.CheckGround() && movement.Velocity.y < 0)
            currentJumpCount = 0;
    }

    private void Move(Vector2 inputVector)
    {
        movement?.SetMoveVector(inputVector, moveSpeed);
    }

    private void Jump()
    {
        if (!movement.CheckGround())
        {
            if (currentJumpCount >= maxJumpCount)
                return;

            if (currentJumpCount == 0)
                currentJumpCount++;
        }

        float jumpPower = Mathf.Sqrt(jumpHeight * 2 * -2f * gravityScale);
        movement.SetVerticalVelocity(jumpPower);
        currentJumpCount++;
    }

    private void DashHandle()
    {
        controller.ChangeState(PlayerStateType.Dash);
    }
}
