using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionEndHit : AIDecision
{
    public override bool MakeADecision()
    {
        return !_stateInfo.IsHit;
    }
}
