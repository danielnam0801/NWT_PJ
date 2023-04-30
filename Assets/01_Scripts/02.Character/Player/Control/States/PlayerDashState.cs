using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float lastDashTime;
    //[SerializeField]
    //private float maxDashCount;

    //private int dashCount = 0;

    public override void EnterState()
    {
        #region 대쉬 사용 횟수로 제한
        //if(movement.CheckGround())
        //{
        //    dashCount = 0;
        //}
        //else
        //{
        //    if(dashCount >= maxDashCount)
        //    {
        //        controller.ChangeState(PlayerStateType.Movement);
        //        return;
        //    }
        //}
        #endregion

        if (Time.time - lastDashTime < status.DashCoolTime)
        {
            controller.ChangeState(PlayerStateType.Movement);
            return;
        }

        movement.ApplyGravity = false;
        StartCoroutine(Dash());
    }

    public override void ExitState()
    {
        movement.ApplyGravity = true;
        movement?.SetMoveVector(Vector2.zero, 0);
    }

    public override void UpdateState()
    {
        
    }

    private IEnumerator Dash()
    {
        movement.SetMoveVector(transform.right, status.DashSpeed);
        //dashCount++;

        yield return new WaitForSeconds(status.DashDistance / status.DashSpeed);

        lastDashTime = Time.time;
        controller.ChangeState(PlayerStateType.Movement);
    }
}
