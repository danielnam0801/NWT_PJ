using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected AIBrain _brain;

    public UnityEvent AttackFeedBack;
    [SerializeField]
    protected float _attackDelay;
    public float AttackDelay
    {
        get => _attackDelay;
        set => _attackDelay = Mathf.Clamp(value, 0f, 10f);
    }

    protected bool _waitBeforeNextAttack;
    public bool WaitBeforeNextAttack { get => _waitBeforeNextAttack; }

    protected virtual void Awake()
    {
        _brain = GetComponent<AIBrain>();
        
    }

    public abstract void Attack(float damage);

    protected IEnumerator WaitBeforeAttackCoroutine()
    {
        Debug.Log(_attackDelay + ": AttackDel");
        _waitBeforeNextAttack = true;
        yield return new WaitForSeconds(_attackDelay);
        Debug.Log("Del");

        _waitBeforeNextAttack = false;
        _brain.AIActionData.isAttack = false;// 나중에 애니메이션 끝 이벤트때 실행되게 바꿔줘야함
    }

}
