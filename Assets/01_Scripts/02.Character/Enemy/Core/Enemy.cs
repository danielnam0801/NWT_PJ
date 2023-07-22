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
    [field: SerializeField]
    public UnityEvent HitFeedback { get; set; }

    public bool IsDead = false;
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
    public List<SpriteRenderer> ActiveVisual = new List<SpriteRenderer>();
    public SpriteRenderer TestYong;
    [SerializeField] private float _addForcePower = 5f;

    [SerializeField]
    private List<EnemyChildSprite> _enemyChildSlicedSprites = new List<EnemyChildSprite>();
    
    [Space(30)]
    [Tooltip("Slice �����ϸ� true�� ����")]
    public bool isCanSliced = false;

    private Rigidbody2D rigidbody;

    void Awake()
    {
        _brain = GetComponent<AIBrain>();
        _bodyColider = GetComponent<Collider2D>();
        //TestYong = GameObject.Find("TestYong").GetComponent<SpriteRenderer>();
        _rayPoint = transform.Find("RayPoint").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        Transform visualTrm = transform.Find("Visual").transform;
        _enemyAnim = visualTrm.GetComponent<EnemyAgentAnimator>();
        visualTrm.GetComponentsInChildren<SpriteRenderer>(ActiveVisual);
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
        for (int i = 0; i < ActiveVisual.Count; i++)
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


    public void GetHit(float damage, GameObject damageDealer, Vector3 HitNormal)
    {
        Debug.Log($"Player���� �¾��ǿ� : {damageDealer.name}");
        if (IsDead == true) return;

        Health -= damage;

        if (Health <= 0)
        {
            DeadProcess();
            return;
        }

        _brain.AIActionData.HitNormal = HitNormal;
        this.HitPoint = damageDealer.transform.position;

        PoolManager.Instance.Pop("AttackEffect");

        if(!_brain.AIStateInfo.IsAttack && !_brain.AIStateInfo.IsAttackWait)
            OnGetHit?.Invoke();
        HitFeedback?.Invoke();

    }
    private void DeadProcess()
    {
        Health = 0;
        IsDead = true;
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
            //PoolManager.Instance.Push(this);
            #region Ǯ������ �ٲܺκ�
            //Destroy(gameObject);
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

    public void SetGravityScale(float value)
    {
        rigidbody.gravityScale = value;
    }

    public override void Init()
    {
        IsDead = false;
        Health = _enemyDataSO.HP;
        _brain.Init();
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
