using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAction : AIAction
{
    public SkillName skillName;
    public UnityEvent EndEvent;

    [Tooltip("���⼭ false�� �����ϸ� �̿� �����ϴ� EnemyAttack���� Animation������ ����� ��")]
    public bool isAnimTriggerOn = true; //�� ���� �����°��� �ִϸ��̼� Ʈ���ŷ� ��������, �ƴϸ� ��ũ��Ʈ�� ��������
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
