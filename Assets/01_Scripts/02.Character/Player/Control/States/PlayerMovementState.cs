using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerState
{
    private int currentJumpCount = 0;


    private float gravityScale = -9.81f;

    public override void EnterState()
    {
        input.OnMovementInput += Move;
        input.OnSpaceInput += Jump;
        input.OnShiftInput += DashHandle;
        input.OnLeftClickInput += TeleportationHandle;
    }

    public override void ExitState()
    {
        input.OnMovementInput -= Move;
        input.OnSpaceInput -= Jump;
        input.OnShiftInput -= DashHandle;
        input.OnLeftClickInput -= TeleportationHandle;
        movement?.SetMoveVector(Vector2.zero, 0);
    }

    public override void UpdateState()
    {
        if (movement.CheckGround() && movement.Velocity.y < 0)
            currentJumpCount = 0;
    }

    private void Move(Vector2 inputVector)
    {
        movement?.SetMoveVector(inputVector, status.MoveSpeed);
    }

    private void Jump()
    {
        if (!movement.CheckGround())
        {
            if (status.MaxJumpCount == 1)
                return;

            if (currentJumpCount >= status.MaxJumpCount)
                return;

            if (currentJumpCount == 0)
                currentJumpCount++;
        }

        float jumpPower = Mathf.Sqrt(status.JumpHeight * 2 * -2f * gravityScale);
        movement.SetVerticalVelocity(jumpPower);
        currentJumpCount++;
    }

    private void DashHandle()
    {
        controller.ChangeState(PlayerStateType.Dash);
    }

    private void TeleportationHandle()
    {
        if (attack.Weapon.IsStay)
            controller.ChangeState(PlayerStateType.Teleportation);
    }
}
