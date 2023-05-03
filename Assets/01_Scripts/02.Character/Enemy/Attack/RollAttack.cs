using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAttack : EnemyAttack
{
    public override void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        AttackStartFeedback?.Invoke();
        
        
    }
}
