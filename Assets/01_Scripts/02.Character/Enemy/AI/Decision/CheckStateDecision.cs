using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStateDecision : AIDecision
{
    [SerializeField] AIState baseState;
    public override bool MakeADecision()
    {
        return _brain.CurrentState == baseState;
    }
}
