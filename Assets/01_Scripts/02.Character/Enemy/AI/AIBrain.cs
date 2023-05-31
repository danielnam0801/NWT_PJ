using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> AttackAndChaseStateChanged;
    public UnityEvent<Vector2, Vector2> IdleStateStateChanged;
    public UnityEvent OnFireButtonPress;

    [SerializeField]
    private AIState _currentState;

    [SerializeField]
    private Transform _target;
    public Transform Target => _target;
    [SerializeField]
    private Transform _basePos;
    [SerializeField]
    LayerMask GroundLayer;
    public Transform BasePos => _basePos;
    public AIActionData AIActionData { get; private set; }
    public AIMovementData AIMovementData { get; private set; }
   
    private EnemyMovement _enemyMovement;
    public EnemyMovement EnemyMovement => _enemyMovement;

    protected AttackCoolController _attackCoolController;
    public AttackCoolController AttackCoolController => _attackCoolController;
    Enemy enemy;
    public Enemy Enemy => enemy;

    public EnemyAgentAnimator _enemyAnim { get; private set; }
    //public GroundEnemyAnim GroundEnemyAnim { get => _groundEnemyAnim; }
    private AIStateInfo _stateInfo;
    AIState hitState;

    protected virtual void Awake()
    {
        
        enemy = transform.GetComponent<Enemy>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _attackCoolController = GetComponent<AttackCoolController>();

        Transform rootAI = transform.Find("AI").transform;
        AIActionData = rootAI.GetComponent<AIActionData>();
        Debug.Log(rootAI);
        AIMovementData = rootAI.GetComponent<AIMovementData>();
        _stateInfo = rootAI.GetComponent<AIStateInfo>();
        hitState = rootAI.Find("HitState").GetComponent<AIState>();
    }
    private void Start()
    {
        _target = GameManager.instance.Target;
    }

    protected void Update()
    {
        if (_target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
            return;
        }
        else
        {
            _currentState.UpdateState();
        }
    }

    public Vector3 GetTargetUnderPosition()
    {
        RaycastHit2D ray = Physics2D.Raycast(_target.position, Vector2.down, 5f, GroundLayer);
        if (ray.collider != null) return ray.point;
        else return _target.position;
    }
    public void ChangeState(AIState state)
    {
        _currentState.ExitState();
        _currentState = state;
        _currentState.InitState();
    }

    public void Move(Vector2 direction, Vector3 targetPos)
    {
        OnMovementKeyPress?.Invoke(direction);
        if (!AIActionData.isIdle)
            AttackAndChaseStateChanged?.Invoke(targetPos);
        else
            IdleStateStateChanged?.Invoke(AIMovementData.direction, AIMovementData.beforeDirection);
    }

    public virtual void Attack(SkillType skillName)
    {
        _attackCoolController.Attack(skillName);
    }
    
    public void ChangeToHitState()
    {
        if(_currentState != hitState && _stateInfo.IsAttack == false)
        {
            ChangeState(hitState);
        }
    }
}
