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

    public override void ExitAction() // �ִϸ��̼� ���ൿ�� �� ���� ������ �׳� ������
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
