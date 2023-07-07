using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EndOfAttackDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return _aiStateInfo.IsAttack == false;
    }
}