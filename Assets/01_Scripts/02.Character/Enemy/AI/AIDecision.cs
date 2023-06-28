using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIBrain _brain;
    protected AIStateInfo _stateInfo;
    protected AIActionData _aiActionData;
    protected AIMovementData _aiMovementData;
    public bool isReverse = false;

    protected virtual void Awake()
    {
        _brain = transform.parent.parent.parent.GetComponent<AIBrain>();
        _stateInfo = _brain.transform.Find("AI").GetComponent<AIStateInfo>();

        _aiActionData = _brain.transform.Find("AI").GetComponent<AIActionData>();
        _aiMovementData = _brain.transform.Find("AI").GetComponent<AIMovementData>();
    }

    public abstract bool MakeADecision();
}

