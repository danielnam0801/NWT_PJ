using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public SkillName skillName;
    public override void Init()
    {

    }

    public override void TakeAction()
    {
        _brain.Attack(skillName);
        Debug.Log("name : " + skillName);
    }
}
