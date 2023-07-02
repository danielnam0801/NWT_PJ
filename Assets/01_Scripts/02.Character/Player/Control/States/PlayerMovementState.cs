using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerState
{
    private int currentJumpCount = 0;
    [SerializeField]
    private float transDashTime = 10f;

    private Vector2 lastMoveVector = Vector2.zero;
    private Vector2 newMoveVector = Vector2.zero;
    private float lastMoveTime = 0;
    private float newMoveTime = 0;

    private float gravityScale = -9.81f;

    public override void EnterState()
    {
        input.OnMovementInput += Move;
        input.OnSpaceInput += Jump;
        input.OnLeftClickInput += TeleportationHandle;
        input.OnShiftInput += DashHandle;
    }

    public override void ExitState()
    {
        input.OnMovementInput -= Move;
        input.OnSpaceInput -= Jump;
        input.OnLeftClickInput -= TeleportationHandle;
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
        movement?.SetMoveVector(inputVector, status.MoveSpeed);

        if(inputVector.sqrMagnitude == 0)
        {
            anim.SetMove(0);
        }
        else
        {
            anim.SetMove(status.MoveSpeed);
        }

        //newMoveVector = inputVector;
        //lastMoveVector = newMoveVector;

        //lastMoveTime = newMoveTime;
        //newMoveTime = Time.time;

        //if (GetInputDiff() <= transDashTime)
        //{
        //    if (newMoveVector != Vector2.zero && lastMoveVector == newMoveVector)
        //        DashHandle();
        //}
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

        anim.PlayJumpAnimation();
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

    private float GetInputDiff()
    {
        return newMoveTime - lastMoveTime;
    }
}
