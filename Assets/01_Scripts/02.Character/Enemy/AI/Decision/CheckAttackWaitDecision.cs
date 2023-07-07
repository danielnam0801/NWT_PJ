using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackWaitDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return _aiStateInfo.IsAttackWait;
    }
}
