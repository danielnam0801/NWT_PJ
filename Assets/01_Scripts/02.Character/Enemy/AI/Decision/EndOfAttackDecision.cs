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
        // 너무 느리면 이거 빼고 Decision쪽 isAttack == false로 해줘도 되긴함, 대신 AttackCoolController로 상태들을 다 옮겨 줘야 함
    }

    public override bool MakeADecision()
    {
        return (bool)skillInfo.GetValue(_state) == false;
    }
}