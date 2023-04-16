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
    protected EnemyAgentAnimator _enemyAnim;
    public EnemyAgentAnimator EnemyAnimator => _enemyAnim;
    Rigidbody2D rb;

    void Awake()
    {
        _brain  = GetComponent<AIBrain>();
        _enemyAnim = transform.Find("Visual").GetComponent<EnemyAgentAnimator>();
    }

    void Start()
    {
        SetEnemyData();
        //OnDie.AddListener(Die);
    }

    private void SetEnemyData()
    {
        Health = _enemyDataSO.HP;
        _brain.AIMovementData.thinkTime = _enemyDataSO.ThinkTime;
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        Debug.Log("Player���� �¾��ǿ�");
        if (_isDead == true) return;


        Health -= damage;
        Debug.Log(Health);

        HitPoint = damageDealer.transform.position;

        OnGetHit?.Invoke();

        if (Health <= 0)
            DeadProcess();
        _enemyAnim.SetDamageHash(Health);
    }

    private void DeadProcess()
    {
        Health = 0;
        _isDead = true;
        Debug.Log("die");
        _enemyAnim.SetDeadHash(true);
        _enemyAnim.OnAnimaitionEndTrigger += Die;
        OnDie?.Invoke();

    }

    public void Die()
    {
        Debug.Log("die2");
        _enemyAnim.OnAnimaitionEndTrigger -= Die;
        Destroy(gameObject);
    }
}
