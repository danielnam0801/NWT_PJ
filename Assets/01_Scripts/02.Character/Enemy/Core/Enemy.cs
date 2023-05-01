using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Enemy : PoolableObject, IHitable, IAgent
{
    public bool IsEnemy => true;
    public Vector3 HitPoint { get; set; }

    [SerializeField] EnemyDataSO _enemyDataSO;
    public EnemyDataSO EnemyData => _enemyDataSO;

    [field: SerializeField]
    public float Health { get; set; }

    [field: SerializeField]
    public UnityEvent InitAction { get; set; }
    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    protected bool _isDead = false;
    [SerializeField] protected bool _isActive = false;

    protected AIBrain _brain;
    protected EnemyMovement _enemyMovement;
    protected CapsuleCollider2D _bodyColider;
    protected SpriteRenderer _spriteRenderer = null;
    protected EnemyAgentAnimator _enemyAnim;
    Transform _rayPoint;
    public Transform RayPoint => _rayPoint;

    public EnemyAgentAnimator EnemyAnimator => _enemyAnim;
    public LightTwinkle _enemyLight;

    [Header("Slice ����")]
    public EnemyParts[] ActiveVisual;
    public SpriteRenderer TestYong;
    [SerializeField] private float _addForcePower = 5f;


    void Awake()
    {
        _brain = GetComponent<AIBrain>();
        _enemyAnim = transform.Find("Visual").GetComponent<EnemyAgentAnimator>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
        TestYong = GameObject.Find("TestYong").GetComponent<SpriteRenderer>();
        _rayPoint = transform.Find("RayPoint").transform;
    }

    void Start()
    {
        SetEnemyData();
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


        if (Health <= 0)
        {
            DeadProcess();
            return;
        }

        OnGetHit?.Invoke();
    }

    private void DeadProcess()
    {
        Health = 0;
        _isDead = true;
        //_enemyAnim.OnAnimaitionEndTrigger += DieAnimEvent; // Ŀ�� �����̸� �ְ� �ʹٸ� �̰� ����
        // Ŀ�� �����̸� �ְ� �ʹٸ� �̰� ����

        DieEvent();
        OnDie?.Invoke();
        _enemyAnim.SetDeadHash(true);
        _enemyAnim.SetDeathTriggerHash();

    }

    public void DieAnimEvent()
    {
        _enemyAnim.OnAnimaitionEndTrigger -= DieEvent;
    }

    public void DieEvent()
    {
        CreateCanSlicedObject();
    }

    private void CreateCanSlicedObject()
    {
        foreach (EnemyParts eP in ActiveVisual)
        {
            eP.CreateSameObject();    
        }
    }

    public override void Init()
    {
        _isDead = false;
        Health = _enemyDataSO.HP;
        _enemyAnim.Init();
        InitAction?.Invoke();
        foreach(EnemyParts eP in ActiveVisual)
        {
            eP.SetSpriteRenderEnabled(true);
        }
    }

    public void PushingObj(float delay)
    {
        StartCoroutine(DelayCoroutine(delay, action: ()=>
        {
            this.gameObject.SetActive(false);
            //PoolManager.Instance.Push(this);
            Debug.Log("Pooled");
        }));
    }

    private IEnumerator DelayCoroutine(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
