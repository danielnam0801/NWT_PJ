using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions;
    [SerializeField]
    private AIState nextState;
    public AIState NextState => nextState;

    protected virtual void Awake()
    {
        GetComponents<AIDecision>(decisions);
    }
}
