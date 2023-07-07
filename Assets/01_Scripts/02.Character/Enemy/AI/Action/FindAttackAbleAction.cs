using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAttackAbleAction : AIAction
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

    public override void ExitAction() { }
    public override void InitAction() { }

    public override void TakeAction()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (!_stateInfo.IsAttack)
            {
                _brain.Attack(skills[i]);
            }
        }
    }
}
