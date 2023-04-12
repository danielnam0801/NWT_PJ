using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    private PlayerMovement movement;

    protected override void Awake()
    {
        base.Awake();
        movement = controller.GetComponent<PlayerMovement>();
    }

    public override void EnterState()
    {
        movement?.StopImmediately();
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }
}
