using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHitable, IAgent
{
    public bool IsEnemy => true;
    public Vector3 HitPoint { get; set; }

    [SerializeField] EnemyDataSO _enemyDataSO;
    public EnemyDataSO EnemyData => _enemyDataSO;
    
    [field : SerializeField]
    public float Health { get; set; }

    [field : SerializeField]
    public UnityEvent OnDie { get; set; }
    [field : SerializeField]
    public UnityEvent OnGetHit { get; set; }
    
    protected bool _isDead = false;
    [SerializeField] protected bool _isActive = false;
    
    protected AIBrain _brain;
    protected EnemyMovement _enemyMovement;
    protected CapsuleCollider2D _bodyColider;
    protected SpriteRenderer _spriteRenderer = null;
    protected EnemyAgentAnim _enemyAnim;
    Rigidbody2D rb;

    void Awake()
    {
        _brain  = GetComponent<AIBrain>();
    }

    void Start()
    {
        SetEnemyData();
        OnDie.AddListener(Die);
    }

    private void SetEnemyData()
    {
        Debug.Log(_brain.gameObject.name +_brain.AIMovementData +  " + !");
        Health = _enemyDataSO.HP;
        _brain.AIMovementData.thinkTime = _enemyDataSO.ThinkTime;
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        Debug.Log("PlayerÇÑÅ× ¸Â¾ÒÂÇ¿°");
        if (_isDead == true) return;


        Health -= damage;
        Debug.Log(Health);

        HitPoint = damageDealer.transform.position;

        OnGetHit?.Invoke();

        if (Health <= 0)
            DeadProcess();
    }

    private void DeadProcess()
    {
        Health = 0;
        _isDead = true;
        Debug.Log("die");
        //_enemyAnim.PlayDeadAnimation();
        OnDie?.Invoke();
    }

    public void Die()
    {
        Debug.Log("die2");
        Destroy(gameObject);
    }
}
