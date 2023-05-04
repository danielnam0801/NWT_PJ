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
    protected CapsuleCollider2D _bodyColider;
    protected EnemyAgentAnimator _enemyAnim;
    Transform _rayPoint;
    public Transform RayPoint => _rayPoint;

    public EnemyAgentAnimator EnemyAnimator => _enemyAnim;
    public LightTwinkle _enemyLight;

    [Header("childSprite")]
    public List<SpriteRenderer> spriteRenders;
    
    [Header("DissolveEffect")]
    public DissolveEffect _dissolveType;
    [SerializeField] float _dissolveDelay = 0.5f;
    [SerializeField] float _dissolvePlayTime = 2.5f;
    [SerializeField] float _dissolveSequenceTime = 0.3f;


    [Header("Slice 관련")]
    public EnemyParts[] ActiveVisual;
    public SpriteRenderer TestYong;
    [SerializeField] private float _addForcePower = 5f;


    void Awake()
    {
        _brain = GetComponent<AIBrain>();
        _enemyAnim = transform.Find("Visual").GetComponent<EnemyAgentAnimator>();
        //TestYong = GameObject.Find("TestYong").GetComponent<SpriteRenderer>();
        _rayPoint = transform.Find("RayPoint").transform;
    }

    void Start()
    {
        SetEnemyData();
        SpriteMaterialInit();
    }

    private void SpriteMaterialInit()
    {
        for (int i = 0; i < spriteRenders.Count; i++)
        {
            if (spriteRenders[i] != null)
            {
                spriteRenders[i].material.SetFloat("_Dissolve", 1);
            }
        }
    }

    private void SetEnemyData()
    {
        Health = _enemyDataSO.HP;
        //Debug.Log(_brain.AIMovementData);
        _brain.AIMovementData.thinkTime = _enemyDataSO.ThinkTime;
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        Debug.Log("Player한테 맞았쪄염");
        if (_isDead == true) return;

        Health -= damage;
        Debug.Log(Health);

        HitPoint = damageDealer.transform.position;

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
        _enemyAnim.OnAnimaitionEndTrigger += DieAnimEvent; // 커팅 딜레이를 주고 싶다면 이걸 켜주
        // 커팅 딜레이를 주고 싶다면 이걸 켜주

        OnDie?.Invoke();
        _enemyAnim.SetDeadHash(true);
        _enemyAnim.SetDeathTriggerHash();

    }

    private void DieAnimEvent()
    {
        DieEvent();
    }

    public void DieEvent()
    {
        //CreateCanSlicedObject(); // 자르는거 쓸꺼면 이거 활성화 근데 아직 안될꺼임
        Sequence seq = DOTween.Sequence();
        float time = _dissolveDelay;
        switch (_dissolveType)
        {
            case DissolveEffect.Whole:

                seq.PrependInterval(time); // 시작 딜레이

                foreach (var a in spriteRenders)
                {
                    Tween dissolve = DOTween.To(
                        () => a.material.GetFloat("_Dissolve"),
                        x => a.material.SetFloat("_Dissolve", x),
                        0f,
                        _dissolvePlayTime);

                    seq.Join(dissolve);
                }
                break;
            case DissolveEffect.Each:
                foreach(var a in spriteRenders)
                {
                    Tween dissolve = DOTween.To(
                        () => a.material.GetFloat("_Dissolve"),
                        x => a.material.SetFloat("_Dissolve", x),
                        0f,
                        _dissolvePlayTime);

                    seq.Insert(time,dissolve);
                    time += _dissolveSequenceTime;
                }
                break;
        }
        seq.OnComplete(() =>
        {
            _enemyAnim.OnAnimaitionEndTrigger -= DieAnimEvent;
            Destroy(gameObject);
        });
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
