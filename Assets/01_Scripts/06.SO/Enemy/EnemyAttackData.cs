using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/EnemyAttackData")]
public class EnemyAttackData : ScriptableObject
{
    public EnemyAttack atk;
    public SkillName AttackName;
    public float coolTime;
    public float damage;    
}