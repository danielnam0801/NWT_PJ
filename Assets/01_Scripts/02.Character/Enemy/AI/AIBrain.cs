using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    #region º¯¼öµé   
    public UnityEvent<Vector2> OnMovementKeyPress;

    [SerializeField]
    private AIState _currentState;
    public AIState CurrentState => _currentState;
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
    public AIStateInfo AIStateInfo { get; private set; }
  
    private EnemyMovement _enemyMovement;
    public EnemyMovement EnemyMovement => _enemyMovement;

    protected EnemyAttackController _attackCoolController;
    public EnemyAttackController AttackCoolController => _attackCoolController;
    Enemy enemy;
    public Enemy Enemy => enemy;
    public EnemyAgentAnimator _enemyAnim { get; private set; }

    private List<AITransition> _anyTransitions = new List<AITransition>();
    public List<AITransition> AnyTransitions => _anyTransitions;

    public bool UseBrain { get; set; } = true;
    AIState firstState;
    #endregion

    protected virtual void Awake()
    {
        enemy = transform.GetComponent<Enemy>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _attackCoolController = GetComponent<EnemyAttackController>();
        _enemyAnim = transform.Find("Visual").GetComponent<EnemyAgentAnimator>();

        Transform rootAI = transform.Find("AI").transform;
        AIActionData = rootAI.GetComponent<AIActionData>();
        AIMovementData = rootAI.GetComponent<AIMovementData>();
        AIStateInfo = rootAI.GetComponent<AIStateInfo>();
        firstState = rootAI.Find("IdleState").GetComponent<AIState>();

        Transform anyTranTrm = transform.Find("AI/AnyTransitions");
        if (anyTranTrm != null)
        {
            anyTranTrm.GetComponentsInChildren<AITransition>(_anyTransitions);
        }
    }
    private void Start()
    {
        _target = GameManager.instance.Target;
        _currentState.InitState();
    }
    protected void Update()
    {
        if (_target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
            return;
        }
        else if(UseBrain)
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
    }

    public virtual bool Attack(SkillType skillName)
    {
        return _attackCoolController.Attack(skillName);
    }

    public void Init()
    {
        _currentState = firstState;
    }
}
