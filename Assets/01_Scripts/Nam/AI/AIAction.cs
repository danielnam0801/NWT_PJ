using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIBrain _brain;
    protected AIActionData _aiActionData;
    protected AIMovementData _aiMovementData;

    protected virtual void Awake()
    {
        _brain = transform.parent.parent.GetComponent<AIBrain>();
        _aiActionData = transform.parent.GetComponent<AIActionData>();
        _aiMovementData = transform.parent.GetComponent<AIMovementData>();
    }

    public abstract void TakeAction();
}
