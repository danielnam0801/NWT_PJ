using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public SkillName skillName;
    public override void InitAction()
    {

    }

    public override void TakeAction()
    {
        _brain.Attack(skillName);
        Debug.Log("name : " + skillName);
    }

    public override void ExitAction()
    {

    }
}
