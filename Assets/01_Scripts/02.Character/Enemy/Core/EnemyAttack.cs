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
    protected AttackCoolController _CoolController;

    [SerializeField]
    protected SkillType _skillName;
    public UnityEvent AttackStartFeedback;
    public UnityEvent AttackEndFeedback;
    protected Action callBack = null;

    protected virtual void Awake()
    {
        _brain = transform.parent.GetComponent<AIBrain>();
        _stateInfo = transform.parent.Find("AI").GetComponent<AIStateInfo>();
        _animator = _brain.transform.Find("Visual").GetComponent<EnemyAgentAnimator>();
        _aiActionData = _brain.transform.Find("AI").GetComponent<AIActionData>();
        _CoolController = _brain.GetComponent<AttackCoolController>();
    }

    protected IEnumerator DelayCoroutine(float afterAttackDelay, Action afterPlayAction)
    {
        yield return new WaitForSeconds(afterAttackDelay);
        afterPlayAction();
    }

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
