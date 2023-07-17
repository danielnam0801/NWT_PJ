using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBeeHitAction : AIAction
{
    [SerializeField] float angryStateTime = 5f;
    float t = 0;
    bool returnIdle = false;
    int hitCnt = 0;

    public override void InitAction()
    {
        Debug.Log("Hittt");
        returnIdle = false;
        t = 0;
        _stateInfo.IsAttack = true;
        _animator.OnAnimaitionEndTrigger += EndAnim;
        _animator.SetAttackState(true);
        _animator.SetDamageHash(_brain.Enemy.Health);
    }

    public override void TakeAction()
    {
        int dirX = (_brain.Target.position.x - transform.position.x > 0) ? 1 : -1;
        _aiMovementData.direction = new Vector2(
            dirX,
            _aiMovementData.direction.y
        );

        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);

        if (_stateInfo.HitCnt != hitCnt)
        {
            if(hitCnt != 0) _animator.HitHash();
            t = 0;
            hitCnt = _stateInfo.HitCnt;
        }

        if(t < angryStateTime)
        {
            t += Time.deltaTime;
        }
        else
        {
            returnIdle = true;
        }

    }

    public override void ExitAction() // �ִϸ��̼� ���ൿ�� �� ���� ������ �׳� ������
    {
        _animator.OnAnimaitionEndTrigger -= EndAnim;
        _animator.SetEndHit();
        _animator.SetAttackState(false);
        _stateInfo.InitHitCount();
    }

    public void EndAnim()
    {
        if (returnIdle)
        {
            _stateInfo.IsHit = false;
            _stateInfo.IsAttack = false;
        }
    }

}
