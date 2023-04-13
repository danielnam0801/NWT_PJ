using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIBrain _brain;
    protected AIStateInfo _stateInfo;
    protected AIActionData _aiActionData;
    protected AIMovementData _aiMovementData;

    protected virtual void Awake()
    {
        _brain = transform.parent.parent.GetComponent<AIBrain>();
        _stateInfo = transform.parent.GetComponent<AIStateInfo>();
        _aiActionData = transform.parent.GetComponent<AIActionData>();
        _aiMovementData = transform.parent.GetComponent<AIMovementData>();
    }

    public abstract void Init();
    public abstract void TakeAction();

    protected IEnumerator DelayCoroutine(float delayTime ,Action action)
    {
        yield return new WaitForSeconds(delayTime);
        action?.Invoke();
    }
}
