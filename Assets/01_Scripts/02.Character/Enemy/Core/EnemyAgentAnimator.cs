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

    private readonly int _playLandAnimHash = Animator.StringToHash("landTrigger");
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

    private void Awake()
    {
        _aiStateInfo = transform.parent.Find("AI").GetComponent<AIStateInfo>();
        _animator = GetComponent<Animator>();
        reverseValue = (reverseSprite == true) ? -1 : 1; 
    }

    public void ChaseAttackFaceDirection(Vector2 pointerInput)
    {
        if (_aiStateInfo.IsAttack == false)
        {
            Vector3 direction = (Vector3)pointerInput - transform.position;
            transform.parent.localScale = (direction.x < 0 && reverseSprite == false) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }

    public void IdleFaceDirection(Vector2 currentDir, Vector2 beforeDir)
    {
        if (currentDir.x == 0)
        {
            transform.parent.localScale = (beforeDir.x < 0 && reverseSprite == false) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); //멈춤이 일어날때 전에 보던 방향에 따른 페이스 디렉션
        }
        else
            transform.parent.localScale = (currentDir.x < 0 && reverseSprite == false) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

    }

    public void SetAttackState(bool value)
    {
        _animator.SetBool(_isAttackHash, value);
    }

    public void PlayLandAnimation()
    {
        _animator.SetTrigger(_playLandAnimHash);
    }

    public void SetAttackTrigger(bool value, SkillName Skill)
    {
        if (value)
        {
            switch (Skill)
            {
                case SkillName.Normal:
                    _animator.ResetTrigger(_NormalAttackHash);
                    _animator.SetTrigger(_NormalAttackHash);
                    currentSkillHash = _NormalAttackHash; 
                    break; 
                case SkillName.Special:
                    _animator.ResetTrigger(_SpecialAttackHash);
                    _animator.SetTrigger(_SpecialAttackHash);
                    currentSkillHash = _SpecialAttackHash; 
                    break;
                case SkillName.Melee:
                    _animator.ResetTrigger(_MeleeAttackHash);
                    _animator.SetTrigger(_MeleeAttackHash);
                    currentSkillHash = _MeleeAttackHash; 
                    break;
                case SkillName.Range:
                    _animator.ResetTrigger(_RangeAttackHash);
                    _animator.SetTrigger(_RangeAttackHash);
                    currentSkillHash = _RangeAttackHash; 
                    break;
                default:
                    Debug.LogError("존재하지 않는 스킬");
                    break;
            }
        }
        else
        {
            switch (Skill) {
                case SkillName.Normal:
                    _animator.ResetTrigger(_NormalAttackHash);
                    break;
                case SkillName.Special:
                    _animator.ResetTrigger(_SpecialAttackHash);
                    break;
                case SkillName.Melee:
                    _animator.ResetTrigger(_MeleeAttackHash);
                    break;
                case SkillName.Range:
                    _animator.ResetTrigger(_RangeAttackHash);
                    break;
                default:
                    Debug.LogError("존재하지 않는 스킬");
                    break;
            }

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
    }

    public void SetDamageHash(float currentHp)
    {
        if (currentHp < 0) _animator.SetTrigger(_deadTriggerHash);
        else _animator.SetTrigger(_damageTriggerHash);
    }

    public void OnAnimationEnd()
    {
        if (OnAnimaitionEndTrigger == null) 
            Debug.LogError("sdfdsf");
        OnAnimaitionEndTrigger?.Invoke();
    }

    public void OnAnimationEvent()
    {
        Debug.Log("Event0");
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
}
