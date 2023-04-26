using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;
using UnitySpriteCutter;

public class Enemy : PoolableObject, IHitable, IAgent
{
    public bool IsEnemy => true;
    public Vector3 HitPoint { get; set; }

    [SerializeField] EnemyDataSO _enemyDataSO;
    public EnemyDataSO EnemyData => _enemyDataSO;

    [field: SerializeField]
    public float Health { get; set; }

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
    public EnemyAgentAnimator EnemyAnimator => _enemyAnim;
    Rigidbody2D rb;

    [Header("Slice 관련")]
    public Sprite SlicedSprite;
    public Collider2D[] slicedParts;
    public SpriteRenderer TestYong;
    [SerializeField] private float _addForcePower = 5f;

    void Awake()
    {
        _brain = GetComponent<AIBrain>();
        _enemyAnim = transform.Find("Visual").GetComponent<EnemyAgentAnimator>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
        TestYong = GameObject.Find("TestYong").GetComponent<SpriteRenderer>();
        for(int i = 0; i < slicedParts.Length; i++)
        {
            slicedParts[i].enabled = false; 
        }
        
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
        Debug.Log("Player한테 맞았쪄염");
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
        //_enemyAnim.OnAnimaitionEndTrigger += Die; // 커팅 딜레이를 주고 싶다면 이걸 켜주
        // 커팅 딜레이를 주고 싶다면 이걸 켜주

        DieEvent();
        OnDie?.Invoke();
        _enemyAnim.SetDeadHash(true);
        _enemyAnim.SetDeathTriggerHash();

    }

    public void Die()
    {
        _enemyAnim.OnAnimaitionEndTrigger -= DieEvent;
    }

    public void DieEvent()
    {
        SpriteChangeing();
    }

    private void SpriteChangeing()
    {
        //sprite교체로 바꿀시 사용할 코드
        //this.gameObject.AddComponent<SpriteRenderer>();
        //SpriteRenderer _slicingSprite = GetComponent<SpriteRenderer>();
        //_slicingSprite.sprite = SlicedSprite;
        //_slicingSprite.sortingOrder = 5;
        //this.gameObject.layer = LayerMask.NameToLayer("CanCutted");
        //transform.Find("Visual").gameObject.SetActive(false);
        //transform.Find("center bone").gameObject.SetActive(false);

        foreach (Collider2D go in slicedParts)
        {
            go.gameObject.layer = LayerMask.NameToLayer("CanCutted");
            go.enabled = true;
        }

    }

    public override void Init()
    {
        _isDead = false;
        Health = _enemyDataSO.HP;
        _enemyAnim.Init();
    }

    public void PushingObj(float delay)
    {
        PoolManager.Instance.Push(this);
    }

    // sprite교체로 바꿀시 사용할 코드
    //public void SpriteCutting(Vector2 InputVec, Vector2 OutputVec, int layerMask = -1)
    //{
    //    SpriteCutterOutput output = SpriteCutter.Cut(new SpriteCutterInput()
    //    {
    //        lineStart = InputVec,
    //        lineEnd = OutputVec,
    //        gameObject = this.gameObject,
    //        gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_ONE,
    //    });

    //    if (output != null && output.secondSideGameObject != null)
    //    {
    //        //output.secondSideGameObject.AddComponent<>;
    //        //output.firstSideGameObject.AddComponent<>;
    //        Rigidbody2D newRigidbody = output.secondSideGameObject.AddComponent<Rigidbody2D>();
    //        Rigidbody2D newRigidbody2;
    //        if (output.firstSideGameObject.GetComponent<Rigidbody2D>() == null)
    //            newRigidbody2 = output.firstSideGameObject.AddComponent<Rigidbody2D>();
    //        else
    //            newRigidbody2 = output.secondSideGameObject.GetComponent<Rigidbody2D>();

    //        newRigidbody2.AddForceAtPosition((newRigidbody2.position - InputVec) * 5, InputVec, ForceMode2D.Impulse);
    //        newRigidbody.AddForceAtPosition((newRigidbody.position - InputVec) * 5, InputVec, ForceMode2D.Impulse);
    //    }
    //}
}
