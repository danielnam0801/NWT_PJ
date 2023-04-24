using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnitySpriteCutter;

public class Enemy : MonoBehaviour, IHitable, IAgent, ICuttable
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

    [Header("Slice될 스프라이트 넣어줘야함(죽었을때 실행)")]
    public Sprite SlicedSprite;

    void Awake()
    {
        _brain  = GetComponent<AIBrain>();
        _enemyAnim = transform.Find("Visual").GetComponent<EnemyAgentAnimator>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
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
        SpriteChangeing();
        //_enemyAnim.OnAnimaitionEndTrigger += DieEvent;
        //_enemyAnim.DebugEnd();
        OnDie?.Invoke();    
        //_enemyAnim.SetDeadHash(true);
        //_enemyAnim.SetDeathTriggerHash();

    }

    public void Die()
    {
        _enemyAnim.OnAnimaitionEndTrigger -= DieEvent;
    }

    public void DieEvent()
    {
        SpriteChangeing();
        //_SpriteCutting();
    }

    public void SpriteCutting(Vector2 lineStart, Vector2 lineEnd, int layerMask)
    {
        List<GameObject> gameObjectsToCut = new List<GameObject>();

        gameObjectsToCut.Add(gameObject);
        foreach (GameObject go in gameObjectsToCut)
        {
            Debug.Log("SpriteCut");
            SpriteCutterOutput output = SpriteCutter.Cut(new SpriteCutterInput()
            {
                lineStart = lineStart,
                lineEnd = lineEnd,
                gameObject = go,
                gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_ONE,
            });

            if (output != null && output.secondSideGameObject != null)
            {
                Rigidbody2D newRigidbody = output.secondSideGameObject.AddComponent<Rigidbody2D>();
                newRigidbody.velocity = output.firstSideGameObject.GetComponent<Rigidbody2D>().velocity;
            }
        }
    }

    private void SpriteChangeing()
    {
        this.gameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer _slicingSprite = GetComponent<SpriteRenderer>();
        _slicingSprite.sprite = SlicedSprite;
        _slicingSprite.sortingOrder = 5;
        this.gameObject.layer = LayerMask.NameToLayer("CanCutted");
        GameObject.Find("Visual").SetActive(false);

    }

    bool HitCounts(RaycastHit2D hit)
    {
        return (hit.transform.GetComponent<SpriteRenderer>() != null ||
                 hit.transform.GetComponent<MeshRenderer>() != null);
    }

}
