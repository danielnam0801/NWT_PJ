using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionEndJumpCheck : AIDecision
{
    JumpAttackAction jumpAction;
        
    protected override void Awake()
    {
        base.Awake();
        jumpAction = transform.parent.GetComponent<JumpAttackAction>();
    }

    public override bool MakeADecision()
    {
        return !jumpAction.IsJumping;
    }
}
