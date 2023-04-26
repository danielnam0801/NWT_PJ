using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAction : AIAction
{
    public float hitCnt = 1;
    [SerializeField]
    private float stateHitCnt = 1;
   
    public override void InitAction()
    {
        Debug.Log("Hittt");
        _brain.EnemyMovement.StopImmediatelly();
        _stateInfo.IsHit = true;
        _animator.OnAnimaitionEndTrigger += EndAnim;
        //_animator.SetDamageHash(_brain.Enemy.Health);
        hitCnt = _stateInfo.hitCnt;
    }

    public override void TakeAction()
    {
        if(hitCnt != _stateInfo.hitCnt) // 맞는중에 또맞으면 
        {
            Debug.Log("Attacked");
            hitCnt = _stateInfo.hitCnt;
            _animator.SetDamageHash(_brain.Enemy.Health);
        }
    }

    public override void ExitAction() // 애니메이션 진행동안 또 맞지 않으면 그냥 나가짐
    {
        _animator.OnAnimaitionEndTrigger -= EndAnim;
        _animator.SetEndHit();
    }

    public void EndAnim()
    {
        _stateInfo.IsHit = false;
    }

}
