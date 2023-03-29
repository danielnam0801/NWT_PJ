using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIBrain _brain;
    protected AIActionData _aiActionData;
    protected AIMovementData _aiMovementData;

    protected virtual void Awake()
    {
        _brain = transform.parent.parent.parent.GetComponent<AIBrain>();
        _aiActionData = _brain.transform.Find("AI").GetComponent<AIActionData>();
        _aiMovementData = _brain.transform.Find("AI").GetComponent<AIMovementData>();
    }

    public abstract bool MakeADecision();
}

