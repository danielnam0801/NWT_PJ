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

    protected virtual void Awake()
    {
        //_target = GameManager.instance.Target;
        AIActionData = transform.Find("AI").GetComponent<AIActionData>();
        AIMovementData = transform.Find("AI").GetComponent<AIMovementData>();
        enemy = transform.GetComponent<Enemy>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _attackCoolController = GetComponent<AttackCoolController>();
        //_groundEnemyAnim = transform.Find("VisualSprite").GetComponent<GroundEnemyAnim>();
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
        Debug.DrawRay(_target.position, Vector2.down * 5f, Color.green);
    }

    public Vector3 GetTargetUnderPosition()
    {
        RaycastHit2D ray = Physics2D.Raycast(_target.position, Vector2.down, 5f, GroundLayer);
        if (ray.collider != null) return ray.point;
        else return _target.position;
    }
    public void ChangeState(AIState state)
    {
        state.ExitState();
        _currentState = state;
        state.InitState();
    }

    public void Move(Vector2 direction, Vector3 targetPos)
    {
        OnMovementKeyPress?.Invoke(direction);
        if (!AIActionData.isIdle)
            AttackAndChaseStateChanged?.Invoke(targetPos);
        else
            IdleStateStateChanged?.Invoke(AIMovementData.direction, AIMovementData.beforeDirection);
    }

    public virtual void Attack(SkillName skillName)
    {
        _attackCoolController.Attack(skillName);
    }
}
