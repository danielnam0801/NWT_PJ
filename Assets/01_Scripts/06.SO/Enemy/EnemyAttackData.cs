using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackData
{
    public IEnemyAttack atk;
    public Action action;
    public SkillName AttackName;
    public float coolTime;
    public float damage;
}