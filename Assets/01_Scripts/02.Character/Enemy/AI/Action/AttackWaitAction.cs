using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaitAction : AIAction
{
    public override void InitAction()
    {
        _brain.EnemyMovement.StopImmediatelly();
    }

    public override void TakeAction() {}

    public override void ExitAction() {}
}
