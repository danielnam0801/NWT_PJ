using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{

}

[CreateAssetMenu(menuName = "Assets/SO")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] float m_Speed = 1f;
    [SerializeField] float attackSpeed = 0.5f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float detectRange = 10f;
    [SerializeField] float damage = 1f;

    public float GetSpeed => m_Speed;
    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;
    public float DetectRange => detectRange;
    public float Damage => damage;
    
}
