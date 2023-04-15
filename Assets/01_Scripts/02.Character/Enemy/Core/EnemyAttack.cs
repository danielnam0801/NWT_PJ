using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected AIBrain _brain;
    protected AIStateInfo _stateInfo;   

    public UnityEvent AttackFeedBack;

    [SerializeField] private float playTime;
    public float PlayTime => playTime;

    protected virtual void Awake()
    {
        _brain = transform.parent.GetComponent<AIBrain>();
        _stateInfo = transform.parent.Find("AI").GetComponent<AIStateInfo>();
    }

    public abstract void Attack(Action CallBack);

    protected IEnumerator AttackDamageDelayCoroutine(Action MainAction, float afterAttackDelay, Action afterPlayAction)
    {
        MainAction?.Invoke();
        yield return new WaitForSeconds(afterAttackDelay);
        afterPlayAction?.Invoke();
    }
}
