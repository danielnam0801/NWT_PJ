using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAction : AIAction
{
    public SkillName skillName;
    public UnityEvent EndEvent;

    [Tooltip("여기서 false로 설정하면 이에 상응하는 EnemyAttack에서 Animation실행을 해줘야 함")]
    public bool isAnimTriggerOn = true; //적 공격 끝나는것을 애니메이션 트리거로 통제할지, 아니면 스크립트로 제어할지
    protected AttackCoolController attackCoolController;


    protected override void Awake()
    {
        base.Awake();
        attackCoolController = _brain.transform.GetComponent<AttackCoolController>();
    }

    public override void InitAction()
    {
        _animator.SetSpeed(0);
        if(isAnimTriggerOn)
            SetAnimAttack();
    }


    public override void TakeAction()
    {
        Debug.Log(skillName.ToString());
        _brain.Attack(skillName);
    }

    public override void ExitAction()
    {

    }

    void SetAnimAttack()
    {
        _animator.SetAttackTrigger(true, skillName);
    }
}
