using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected AIBrain _brain;
    protected AIActionData _aiActionData;
    protected EnemyAgentAnimator _animator;
    protected AIStateInfo _stateInfo;
    protected EnemyAttackController _atkController;

    [SerializeField]
    protected SkillType _skillName;
    public UnityEvent AttackStartFeedback;
    public UnityEvent AttackEndFeedback;
    protected Action callBack = null;

    protected float idamage;

    protected virtual void Awake()
    {
        _brain = transform.parent.GetComponent<AIBrain>();
        _stateInfo = transform.parent.Find("AI").GetComponent<AIStateInfo>();
        _animator = _brain.transform.Find("Visual").GetComponent<EnemyAgentAnimator>();
        _aiActionData = _brain.transform.Find("AI").GetComponent<AIActionData>();
        _atkController = _brain.GetComponent<EnemyAttackController>();
    }

    protected IEnumerator DelayCoroutine(float afterAttackDelay, Action afterPlayAction)
    {
        yield return new WaitForSeconds(afterAttackDelay);
        afterPlayAction();
    }

    //public abstract void OnAnimEventAction();

    public void CallbackPlay()
    {
        callBack?.Invoke();
    }

    public void StopAllAct()
    {
        StopAllCoroutines();
        CallbackPlay();
    }
}
