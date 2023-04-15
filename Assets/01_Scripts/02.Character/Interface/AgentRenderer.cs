using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    private readonly int _speedHash = Animator.StringToHash("speed");
    private readonly int _isAirboneHash = Animator.StringToHash("is_airbone");

    private readonly int _attackHash = Animator.StringToHash("attack");
    private readonly int _isAttackHash = Animator.StringToHash("is_attack");

    private readonly int _isRollingHash = Animator.StringToHash("is_rolling");

    public event Action OnAnimaitionEndTrigger = null;
    public event Action OnAnimaitionEventTrigger = null;

    private Animator _animator;
    public Animator Animator => _animator;

    AIMovementData _movementData;
    AIStateInfo _aiStateInfo;


    private void Awake()
    {
        _aiStateInfo = transform.parent.Find("AI").GetComponent<AIStateInfo>();
        _animator = GetComponent<Animator>();
    }

    public void ChaseAttackFaceDirection(Vector2 pointerInput)
    {
        if (_aiStateInfo.IsAttack == false)
        {
            Vector3 direction = (Vector3)pointerInput - transform.position;
            transform.parent.localScale = (direction.x < 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }

    public void IdleFaceDirection(Vector2 currentDir, Vector2 beforeDir)
    {
        if (currentDir.x == 0)
            transform.parent.localScale = (beforeDir.x < 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); //멈춤이 일어날때 전에 보던 방향에 따른 페이스 디렉션
        else
            transform.parent.localScale = (currentDir.x < 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

    }

    public void SetAttackState(bool state)
    {
        _animator.SetBool(_isAttackHash, state);
    }

    public void SetAttackTrigger(bool value)
    {
        if (value) _animator.SetTrigger(_attackHash);
        else _animator.ResetTrigger(_attackHash);
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat(_speedHash, value);
    }

    public void SetAirbone(bool value)
    {
        _animator.SetBool(_isAirboneHash, value);
    }

    public void OnAnimationEnd()
    {
        OnAnimaitionEndTrigger?.Invoke();
    }

    public void OnAnimationEvent()
    {
        OnAnimaitionEventTrigger?.Invoke();
    }
}
