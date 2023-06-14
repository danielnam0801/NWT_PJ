using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAction : AIAction
{
    public SkillType skillName;
    public UnityEvent ExitEvent;
    AIState nextState;

    [Tooltip("���⼭ false�� �����ϸ� �̿� �����ϴ� EnemyAttack���� Animation������ ����� ��")]
    public bool isAnimTriggerOn = true; //�� ���� �����°��� �ִϸ��̼� Ʈ���ŷ� ��������, �ƴϸ� ��ũ��Ʈ�� ��������
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