using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAction : AIAction
{
    public override void InitAction()
    {
        Debug.Log("Hittt");
        _brain.EnemyMovement.StopImmediatelly();
        _animator.OnAnimaitionEndTrigger += EndAnim;
        _animator.SetDamageHash(_brain.Enemy.Health);
    }

    public override void TakeAction()
    {
        _aiMovementData.beforeDirection = new Vector2(_aiMovementData.direction.x, _aiMovementData.direction.y);
    }

    public override void ExitAction() // 애니메이션 진행동안 또 맞지 않으면 그냥 나가짐
    {
        _brain.EnemyMovement.StopImmediatelly();
        _animator.OnAnimaitionEndTrigger -= EndAnim;
        _animator.SetEndHit();
    }

    public void EndAnim()
    {
        _stateInfo.IsHit = false;
    }

}
