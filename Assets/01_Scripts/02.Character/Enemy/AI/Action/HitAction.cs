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
        hitCnt = _stateInfo.HitCnt;
    }

    int hitCnt = 0;
    public override void TakeAction()
    {
        if(_stateInfo.HitCnt != hitCnt)
        {
            hitCnt++;
            _animator.SetDamageHash(_brain.Enemy.Health);
        }
        _aiMovementData.beforeDirection 
            = new Vector2(_aiMovementData.direction.x, _aiMovementData.direction.y);
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
