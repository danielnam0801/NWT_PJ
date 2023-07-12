using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAction : AIAction
{
    public override void InitAction()
    {
        Debug.Log("Hittt");
        hitCnt = _stateInfo.HitCnt;
        //PlayKnockBack();
        _animator.SetDamageHash(_brain.Enemy.Health);
        _brain.EnemyMovement.StopImmediatelly();
        _animator.OnAnimaitionEndTrigger += EndAnim;
    }

    int hitCnt = 0;
    public override void TakeAction()
    {
        if(_stateInfo.HitCnt != hitCnt)
        {
            hitCnt++;
            _animator.SetDamageHash(_brain.Enemy.Health);
            //PlayKnockBack();
        }
    }

    public override void ExitAction() // 애니메이션 진행동안 또 맞지 않으면 그냥 나가짐
    {
        _brain.EnemyMovement.StopImmediatelly();
        _animator.OnAnimaitionEndTrigger -= EndAnim;
    }

    public void EndAnim()
    {
        _animator.SetEndHit();
        _stateInfo.IsHit = false;
    }
}
