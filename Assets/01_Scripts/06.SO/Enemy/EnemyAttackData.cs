using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/EnemyAttackData")]
public class EnemyAttackData : ScriptableObject
{
    public List<EnemyAttack> atk;
    public Action<bool> action;
    public float coolTime;
}