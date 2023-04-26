using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CanAttackDecision : AIDecision
{
    [SerializeField] SkillName Skill;
    FieldInfo fInfo;
    public override bool MakeADecision()
    {
        return (!_state.IsAttack &&_brain.AttackCoolController.isCoolDown(Skill));
    }
}
