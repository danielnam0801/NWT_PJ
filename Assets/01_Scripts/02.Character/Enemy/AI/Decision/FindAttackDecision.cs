using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAttackDecision : AIDecision
{
    List<SkillType> skills = new List<SkillType>();
    private void Start()
    {
        SetSkills(_brain.AttackCoolController.MySkills);
    }
    private void SetSkills(SkillType skilltype)
    {
        foreach (SkillType r in Enum.GetValues(typeof(SkillType)))
        {
            if ((skilltype & r) != 0) skills.Add(r);
        }
    }
    public override bool MakeADecision()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (_brain.FindAttack(skills[i]))
            {
                return true;
            }
        }
        return false;
    }
}
