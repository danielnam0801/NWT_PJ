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
        Debug.LogError("�����ص� ���� ������");
    }


    IEnumerator JumpAttackAndReturn()
    {

        yield return new WaitForSeconds(1f);
        CallbackPlay();
    }


}
