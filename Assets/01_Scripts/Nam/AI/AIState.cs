using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private AIBrain _brain = null;

    [SerializeField] private List<AIAction> _actions;
    [SerializeField] private List<AITransition> _transition = null;

    private void Awake()
    {
        _brain = transform.parent.parent.GetComponent<AIBrain>();
    }

    public void InitState()
    {
        foreach (AIAction action in _actions)
        {
            //Debug.Log(action.name + " : actionName");
            action.Init();
        }
    }

    public void UpdateState()
    {
        foreach (AIAction action in _actions)
        {
            action.TakeAction();
        }

        foreach (AITransition tr in _transition)
        {
            bool result = false;
            foreach (AIDecision d in tr.decisions)
            {
                Debug.Log("Decision : " + d);
                result = d.MakeADecision();
                if (result == false) break;
            }

            if (result == true)
            {
                if (tr.positiveState != null)
                {
                    _brain.ChangeState(tr.positiveState);
                    return;
                }
            }
            else
            {
                if (tr.negativeState != null)
                {
                    _brain.ChangeState(tr.negativeState);
                    return;
                }
            }
        }
    }


}
