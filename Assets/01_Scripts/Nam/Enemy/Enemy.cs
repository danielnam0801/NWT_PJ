using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHitable, IAgent
{
    public bool IsEnemy => true;
    public Vector3 HitPoint { get; set; }

    [SerializeField] EnemyDataSO _enemyDataSO;
    public EnemyDataSO EnemyDataSO => _enemyDataSO;
    
    [field : SerializeField]
    public float Health { get; set; }

    [field : SerializeField]
    public UnityEvent OnDie { get; set; }
    [field : SerializeField]
    public UnityEvent OnGetHit { get; set; }
    
    protected bool _isDead = false;
    [SerializeField] protected bool _isActive = false;
    
    protected AIBrain _brain;
    protected EnemyAttack _attack;
    protected EnemyMovement _enemyMovement;
    protected CapsuleCollider2D _bodyColider;
    protected SpriteRenderer _spriteRenderer = null;
    protected EnemyAgentAnim _enemyAnim;
    Rigidbody2D rb;

    void Awake()
    {

    }

    public virtual void PerformAttack()
    {
        if (_isDead == false && _isActive == true)
        {
            _attack.Attack(_enemyDataSO.Damage);
        }
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        
    }
}
