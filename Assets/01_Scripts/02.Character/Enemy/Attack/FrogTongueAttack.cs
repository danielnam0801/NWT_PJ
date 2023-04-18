using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongueAttack : EnemyAttack
{
    private Action callBack = null;

    public override void Attack(Action CallBack)
    {
        //�� ���� �ִϸ��̼� ������������;
        this.callBack = CallBack;
        StartCoroutine(AttackDamageDelayCoroutine(FrogAttack,PlayTime, DamageCaster));
    }

    void FrogAttack()
    {

    }

    void DamageCaster()
    {
        //Damageüũ����餱

        callBack();
    }
}
