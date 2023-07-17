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
    public bool IsAttackWait = false;
    public bool IsHit { get; set; } = false;
    public bool IsCrash = false;
    
    private int hitcnt = 0;
    public int HitCnt => hitcnt;

    public void PlusHitCount() => hitcnt++;
    public void InitHitCount() => hitcnt = 0;

    public void Init() => IsAttack = IsNormal = IsSpecial = IsMelee = IsRange = false;
}


