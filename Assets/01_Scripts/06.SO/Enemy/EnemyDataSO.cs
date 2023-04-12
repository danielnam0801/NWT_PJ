using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{

}

[CreateAssetMenu(menuName = "Assets/Enemy")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] float m_BeforeSpeed = 1f;
    [SerializeField] float m_AfterSpeed = 3f;
    [SerializeField] float attackSpeed = 0.5f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float detectRange = 10f;
    [SerializeField] float hp = 5f;
    [SerializeField] float thinkTime = 5f;
    public List<EnemyAttackData> _EnemyAttackDatas;

    public float GetBeforeSpeed => m_BeforeSpeed;
    public float GetAfterSpeed => m_AfterSpeed;
    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;
    public float DetectRange => detectRange;
    public float HP => hp;
    public float ThinkTime => thinkTime;
    
}
