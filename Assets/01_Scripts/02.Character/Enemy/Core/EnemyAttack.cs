using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected AIBrain _brain;
    protected EnemyAgentAnimator _animator;
    protected AIStateInfo _stateInfo;

    public UnityEvent AttackStartFeedback;

    [SerializeField] private float afterAttackDelayTime;
    public float AfterAttackDelayTime => afterAttackDelayTime;

    protected Action callBack = null;

    protected virtual void Awake()
    {
        _brain = transform.parent.GetComponent<AIBrain>();
        _stateInfo = transform.parent.Find("AI").GetComponent<AIStateInfo>();
        _animator = _brain.transform.Find("Visual").GetComponent<EnemyAgentAnimator>();
    }

    public abstract void Attack(Action CallBack);

    protected IEnumerator DelayCoroutine(float afterAttackDelay, Action afterPlayAction)
    {
        yield return new WaitForSeconds(afterAttackDelay);
        afterPlayAction();
    }
    
    public void CallbackPlay()
    {
        callBack?.Invoke();
    }
}
