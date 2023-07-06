using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum SkillType
{
    Normal = 1,
    Special = 2,
    Melee = 4,
    Range = 8
}

public class AIStateInfo : MonoBehaviour
{
    [Header("bool")]
    public bool IsAttack = false;
    public bool IsNormal = false;
    public bool IsSpecial = false;
    public bool IsMelee = false;
    public bool IsRange = false;
    public bool IsHit { get; set; } = false;
    public bool IsCrash = false;
    public int hitCnt = 0;

    EnemyAgentAnimator _animator;
    AIMovementData _movementData;

    private void Awake()
    {
        _animator = transform.parent.Find("Visual").GetComponent<EnemyAgentAnimator>();   
        _movementData = transform.GetComponent<AIMovementData>();
    }
    private void Update()
    {
        _animator.SetAttackState(IsAttack);
        _animator.SetSpeed(_movementData.Speed);
    }

    public void PlusHitCount()
    {
        hitCnt++;
    }
}


