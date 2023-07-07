using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAction : AIAction
{
    //[Tooltip("���⼭ false�� �����ϸ� �̿� �����ϴ� EnemyAttack���� Animation������ ����� ��")]
    //public bool isAnimTriggerOn = true; //�� ���� �����°��� �ִϸ��̼� Ʈ���ŷ� ��������, �ƴϸ� ��ũ��Ʈ�� ��������
    public override void InitAction()
    {
        _animator.SetAttackState(true);
        _animator.SetAttackTrigger(_aiActionData.nextSkill);
    }
    public override void TakeAction() { }
    public override void ExitAction()
    {
        _animator.SetAttackState(false);
    }
}