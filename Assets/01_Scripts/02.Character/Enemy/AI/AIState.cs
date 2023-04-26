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
        GetComponents<AIAction>(_actions);
        GetComponentsInChildren<AITransition>(_transition);
    }

    public void InitState()
    {
        foreach (AIAction action in _actions)
        {
            action.InitAction();
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
            if (tr.gameObject.activeSelf == false) continue;
            bool result = false;
            foreach (AIDecision d in tr.decisions)
            {
                result = d.MakeADecision();
                if (d.isReverse == true) result = !result;
                if (result == false) break;
            }
            if (result == true)
                _brain.ChangeState(tr.NextState);
        }
    }

    public void ExitState()
    {
        foreach(AIAction action in _actions)
        {
            action.ExitAction();
        }
    }

}
