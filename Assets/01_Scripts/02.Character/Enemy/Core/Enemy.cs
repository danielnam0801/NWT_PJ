using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;
using UnitySpriteCutter;

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

    [Header("Slice °ü·Ã")]
    public Sprite SlicedSprite;
    public GameObject[] slicedParts;

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
        Debug.Log("PlayerÇÑÅ× ¸Â¾ÒÂÇ¿°");
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

    private void SpriteChangeing()
    {
        //this.gameObject.AddComponent<SpriteRenderer>();
        //SpriteRenderer _slicingSprite = GetComponent<SpriteRenderer>();
        //_slicingSprite.sprite = SlicedSprite;
        //slicingSprite.sortingOrder = 5;
        GameObject.Find("center bone").SetActive(false);
        foreach (GameObject go in slicedParts)
        {
            go.gameObject.layer = LayerMask.NameToLayer("CanCutted");
        }

    }
}
