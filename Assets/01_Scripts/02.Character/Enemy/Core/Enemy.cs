using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DissolveEffect
{
    Whole, Each
}

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
    protected Collider2D _bodyColider;
    protected EnemyAgentAnimator _enemyAnim;
    Transform _rayPoint;
    public Transform RayPoint => _rayPoint;

    public EnemyAgentAnimator EnemyAnimator => _enemyAnim;
    public LightTwinkle _enemyLight;
    public PhysicsMaterial2D physicsMaterial2D;
    
    [Header("DissolveEffect")]
    public DissolveEffect _dissolveType;
    [SerializeField] float _dissolveDelay = 0.5f;
    [SerializeField] float _dissolvePlayTime = 2.5f;
    [SerializeField] float _dissolveSequenceTime = 0.3f;


    [Header("Slice ����")]
    public SpriteRenderer[] ActiveVisual;
    public SpriteRenderer TestYong;
    [SerializeField] private float _addForcePower = 5f;

    [SerializeField]
    private List<EnemyChildSprite> _enemyChildSlicedSprites = new List<EnemyChildSprite>();
    
    [Space(30)]
    [Tooltip("Slice �����ϸ� true�� ����")]
    public bool isCanSliced = false;


    void Awake()
    {
        _brain = GetComponent<AIBrain>();
        _enemyAnim = transform.Find("Visual").GetComponent<EnemyAgentAnimator>();
        _bodyColider = GetComponent<Collider2D>();
        //TestYong = GameObject.Find("TestYong").GetComponent<SpriteRenderer>();
        _rayPoint = transform.Find("RayPoint").transform;
    }

    void Start()
    {
        SetEnemyData();
        SpriteMaterialInit();
    }

    private void SetEnemyData()
    {
        if (isCanSliced)
        {
            foreach(var a in ActiveVisual)
            {
                _enemyChildSlicedSprites.Add(a.GetComponent<EnemyChildSprite>());
            }
        }

        Health = _enemyDataSO.HP;
        Debug.Log(_brain.AIMovementData);
        _brain.AIMovementData.thinkTime = _enemyDataSO.ThinkTime;
    }
    private void SpriteMaterialInit()
    {
        for (int i = 0; i < ActiveVisual.Length; i++)
        {
            if (ActiveVisual[i] != null)
            {
                ActiveVisual[i].material.SetFloat("_Dissolve", 1);
            }
            else
            {
                Debug.Log("Enemy���� spriteRender�ȳ־���");
            }
        }
    }


    public void GetHit(float damage, GameObject damageDealer)
    {
        Debug.Log("Player���� �¾��ǿ�");
        if (_isDead == true) return;

        Health -= damage;
        Debug.Log(Health);

        HitPoint = damageDealer.transform.position;

        PoolManager.Instance.Pop("AttackEffect");
        OnGetHit?.Invoke();

        if (Health <= 0)
        {
            DeadProcess();
            return;
        }
    }

    private void DeadProcess()
    {
        Health = 0;
        _isDead = true;
        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
        _enemyAnim.OnAnimaitionEndTrigger += DieAnimEvent; // Ŀ�� �����̸� �ְ� �ʹٸ� �̰� ����
        // Ŀ�� �����̸� �ְ� �ʹٸ� �̰� ����

        OnDie?.Invoke();
        _enemyAnim.SetDeadHash(true);
        _enemyAnim.SetDeathTriggerHash();

    }

    private void DieAnimEvent()
    {
        DieEvent();
    }

    public void ChangePhysicsMat2D()
    {
        _bodyColider.sharedMaterial = physicsMaterial2D;
    }
    public void InitPhysicsMat2D()
    {
        _bodyColider.sharedMaterial = null;
    }

    public void DieEvent()
    {
        if (isCanSliced)
        {
            CreateCanSlicedObject(); // �ڸ��°� ������ �̰� Ȱ��ȭ �ٵ� ���� �ȵɲ���
        }
        else
        {
            DissolveEffect();
        }
    }

    private void DissolveEffect()
    {
        Sequence seq = DOTween.Sequence();
        float time = _dissolveDelay;
        switch (_dissolveType)
        {
            case global::DissolveEffect.Whole:

                seq.PrependInterval(time); // ���� ������

                foreach (var a in ActiveVisual)
                {
                    Tween dissolve = DOTween.To(
                        () => a.material.GetFloat("_Dissolve"),
                        x => a.material.SetFloat("_Dissolve", x),
                        0f,
                        _dissolvePlayTime);

                    seq.Join(dissolve);
                }
                break;
            case global::DissolveEffect.Each:
                foreach (var a in ActiveVisual)
                {
                    Tween dissolve = DOTween.To(
                        () => a.material.GetFloat("_Dissolve"),
                        x => a.material.SetFloat("_Dissolve", x),
                        0f,
                        _dissolvePlayTime);

                    seq.Insert(time, dissolve);
                    time += _dissolveSequenceTime;
                }
                break;
        }
        seq.OnComplete(() =>
        {
            _enemyAnim.OnAnimaitionEndTrigger -= DieAnimEvent;
            #region Ǯ������ �ٲܺκ�
            Destroy(gameObject);
            #endregion
        });
    }

    private void CreateCanSlicedObject()
    {
        foreach (EnemyChildSprite eP in _enemyChildSlicedSprites)
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
        foreach(EnemyChildSprite eP in _enemyChildSlicedSprites)
        {
            eP.SetSpriteRenderEnabled(true);
        }
        SpriteMaterialInit();
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
