using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillName
{
    Normal,
    Special,
    Jump,
    Melee,
    Range
}
public class AIStateInfo : MonoBehaviour
{
    [Header("bool")]
    public bool IsAttack = false;
    public bool IsNormal = false;
    public bool IsSpecial = false;
    public bool IsJump = false;
    public bool IsMelee = false;
    public bool IsRange = false;

    EnemyAgentAnimator animator;

    private void Awake()
    {
        animator = transform.parent.Find("Visual").GetComponent<EnemyAgentAnimator>();   
    }
    private void LateUpdate()
    {
        animator.SetAttackState(IsAttack);
    }
}


