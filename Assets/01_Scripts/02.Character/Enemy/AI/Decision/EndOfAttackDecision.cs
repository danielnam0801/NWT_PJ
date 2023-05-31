using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EndOfAttackDecision : AIDecision
{
    public SkillType skill;
    private FieldInfo skillInfo;    

    protected override void Awake()
    {
        base.Awake();
        skillInfo = typeof(AIStateInfo).GetField($"Is{skill.ToString()}", BindingFlags.Public | BindingFlags.Instance);
        // �ʹ� ������ �̰� ���� Decision�� isAttack == false�� ���൵ �Ǳ���, ��� AttackCoolController�� ���µ��� �� �Ű� ��� ��
    }

    public override bool MakeADecision()
    {
        return (bool)skillInfo.GetValue(_state) == false;
    }
}