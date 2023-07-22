using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaitAction : AIAction
{
    public override void InitAction()
    {
        _brain.EnemyMovement.StopImmediatelly();
        _stateInfo.IsAttackWait = true;
        StartCoroutine(AttackWait());
    }

    private IEnumerator AttackWait()
    {
        yield return new WaitForSeconds(_aiActionData.attackWaitTime);
        yield return new WaitUntil(() => !_stateInfo.IsHit);
        _stateInfo.IsAttack = true;
        _stateInfo.IsAttackWait = false;
    }

    public override void TakeAction() {}

    public override void ExitAction()
    {
        Vector3 dir = _brain.Target.position - transform.position;
        _aiMovementData.direction.x = dir.x < 0 ? -1 : 1;
        _brain._enemyAnim.Flip();
    }
}
