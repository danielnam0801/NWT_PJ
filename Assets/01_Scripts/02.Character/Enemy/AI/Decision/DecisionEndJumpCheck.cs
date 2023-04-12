using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionEndJumpCheck : AIDecision
{
    JumpAttack jumpAction;
        
    protected override void Awake()
    {
        base.Awake();
        jumpAction = transform.parent.GetComponent<JumpAttack>();
    }

    public override bool MakeADecision()
    {
        return !jumpAction.IsJumping;
    }
}
