using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgentAnimator : MonoBehaviour
{
    private readonly int _speedHash = Animator.StringToHash("speed");

    private readonly int _RangeAttackHash = Animator.StringToHash("rangeAttack");
    private readonly int _MeleeAttackHash = Animator.StringToHash("meleeAttack");
    private readonly int _SpecialAttackHash = Animator.StringToHash("specialAttack");
    private readonly int _NormalAttackHash = Animator.StringToHash("normalAttack");

    private readonly int _isAttackHash = Animator.StringToHash("is_attack");
    private readonly int _deadHash = Animator.StringToHash("isDead");
    private readonly int _deadTriggerHash = Animator.StringToHash("death");
    private readonly int _damageTriggerHash = Animator.StringToHash("damaged");
    private readonly int _EndHitTriggerHash = Animator.StringToHash("EndHit");

    public event Action OnAnimaitionEndTrigger = null;
    public event Action OnAnimaitionEventTrigger = null;

    private Animator _animator;
    public Animator Animator => _animator;

    AIMovementData _movementData;
    AIStateInfo _aiStateInfo;

    [SerializeField]
    bool reverseSprite = false;
    int reverseValue;
    [SerializeField]
    int currentSkillHash;

    [SerializeField] Transform standard;

    private void Awake()
    {
        Transform Ai = transform.parent.Find("AI").transform;
        _aiStateInfo = Ai.GetComponent<AIStateInfo>();
        _movementData = Ai.GetComponent<AIMovementData>();
        _animator = GetComponent<Animator>();
        reverseValue = (reverseSprite == true) ? -1 : 1; 
    }
    
    public void Flip()
    {
        Vector3 value = (_movementData.direction.x < 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        value.x *= reverseValue;
        transform.parent.localScale = value; 

        //Vector3 plusPosValue = new Vector3(0.78f, 0f, 0f);
        //if (value.x == -1)
        //    transform.parent.position -= plusPosValue;
        //else
        //    transform.parent.position += plusPosValue;
    }

    public void SetAttackState(bool value)
    {
        _animator.SetBool(_isAttackHash, value);
    }

    public void SetAttackTrigger(SkillType Skill)
    {
        switch (Skill)
        {
            case SkillType.Normal:
                _animator.ResetTrigger(_NormalAttackHash);
                _animator.SetTrigger(_NormalAttackHash);
                currentSkillHash = _NormalAttackHash;
                break;
            case SkillType.Special:
                _animator.ResetTrigger(_SpecialAttackHash);
                _animator.SetTrigger(_SpecialAttackHash);
                currentSkillHash = _SpecialAttackHash;
                break;
            case SkillType.Melee:
                _animator.ResetTrigger(_MeleeAttackHash);
                _animator.SetTrigger(_MeleeAttackHash);
                currentSkillHash = _MeleeAttackHash;
                break;
            case SkillType.Range:
                _animator.ResetTrigger(_RangeAttackHash);
                _animator.SetTrigger(_RangeAttackHash);
                currentSkillHash = _RangeAttackHash;
                break;
            default:
                Debug.LogError("존재하지 않는 스킬");
                break;
        }   
     }

    public void SetSpeed(float value)
    {
        _animator.SetFloat(_speedHash, value);
    }

    public void SetDeadHash(bool value)
    {
        _animator.SetBool(_deadHash, value);
    }

    public void SetDeathTriggerHash()
    {
        _animator.SetTrigger(_deadTriggerHash);
    }
    public void SetEndHit()
    {
        _animator.SetTrigger(_EndHitTriggerHash);
        _movementData.canMove = true;
    }

    public void SetDamageHash(float currentHp)
    {
        if (currentHp < 0) _animator.SetTrigger(_deadTriggerHash);
        else _animator.SetTrigger(_damageTriggerHash);
    }

    public void HitHash()
    {
        _movementData.canMove = false;
        _animator.SetTrigger("hit");
    }

    public void OnAnimationEnd()
    {
        if (OnAnimaitionEndTrigger == null) 
            Debug.Log("OnAnimationEndTriggerisNull");
        OnAnimaitionEndTrigger?.Invoke();
    }

    public void OnAnimationEvent()
    {
        OnAnimaitionEventTrigger?.Invoke();
    }

    public void DebugEnd()
    {
        Debug.Log(OnAnimaitionEndTrigger.Method);
    }

    public void Init()
    {
        SetDeadHash(false);
        SetSpeed(0);
    }

    public void SetAnimatorSpeed(float speed)
    {
        _animator.SetFloat("AttackSpeed", speed);
    }
}
