using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAndReturnAttack : EnemyAttack, IRangeAttack
{
    public void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        StartCoroutine(JumpAttackAndReturn());
        Debug.LogError("점프앤드 리턴 실행중");
    }


    IEnumerator JumpAttackAndReturn()
    {

        yield return new WaitForSeconds(1f);
        CallbackPlay();
    }


}
