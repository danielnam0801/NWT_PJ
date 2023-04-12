using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerController controller;

    protected virtual void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();   
}
