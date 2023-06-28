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

    EnemyAgentAnimator animator;

    private void Awake()
    {
        animator = transform.parent.Find("Visual").GetComponent<EnemyAgentAnimator>();   
    }
    private void Update()
    {
        animator.SetAttackState(IsAttack);
    }
}


