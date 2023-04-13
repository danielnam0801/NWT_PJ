using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerController controller;
    protected PlayerAnimation anim;
    protected PlayerInput input;
    protected PlayerMovement movement;

    public virtual void Init(Transform root)
    {
        controller = root.GetComponent<PlayerController>();
        movement = root.GetComponent<PlayerMovement>();
        input = root.GetComponent<PlayerInput>();
        anim = root.GetComponent<PlayerAnimation>();
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();   
}
