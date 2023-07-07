using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaitAction : AIAction
{
    public override void InitAction()
    {
    }

    public override void TakeAction()
    {
        _brain.EnemyMovement.StopImmediatelly();
    }

    public override void ExitAction()
    {
        
    }
}
