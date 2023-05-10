using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAndReturnAttack : EnemyAttack, IRangeAttack
{
    private Action action;
    public void Attack(Action CallBack)
    {
        this.action = CallBack;
        StartCoroutine(JumpAttackAndReturn());
    }


    IEnumerator JumpAttackAndReturn()
    {
        yield return null;
    }


}
