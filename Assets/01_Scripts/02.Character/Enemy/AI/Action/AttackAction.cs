using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAction : AIAction
{
    public SkillType skillName;
    public UnityEvent ExitEvent;
    AIState nextState;

    [Tooltip("여기서 false로 설정하면 이에 상응하는 EnemyAttack에서 Animation실행을 해줘야 함")]
    public bool isAnimTriggerOn = true; //적 공격 끝나는것을 애니메이션 트리거로 통제할지, 아니면 스크립트로 제어할지
    protected AttackCoolController attackCoolController;

    protected override void Awake()
    {
        base.Awake();
        nextState = transform.Find("TransChase").GetComponent<AITransition>().NextState;
        attackCoolController = _brain.transform.GetComponent<AttackCoolController>();
    }

    public override void InitAction()
    {
        _animator.SetSpeed(0);
        if(_brain.Attack(skillName) == false)
        {
            _brain.ChangeState(state: nextState);
        }
    }


    public override void TakeAction()
    {
        
    }

    public override void ExitAction()
    {
        ExitEvent?.Invoke();
    }
}