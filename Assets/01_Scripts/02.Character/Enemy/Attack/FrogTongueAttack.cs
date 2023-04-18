using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongueAttack : EnemyAttack
{
    private Action callBack = null;

    public override void Attack(Action CallBack)
    {
        //혀 공격 애니메이션 실행시켜줘야함;
        this.callBack = CallBack;
        StartCoroutine(AttackDamageDelayCoroutine(FrogAttack,PlayTime, DamageCaster));
    }

    void FrogAttack()
    {

    }

    void DamageCaster()
    {
        //Damage체크해줘얗ㅁ

        callBack();
    }
}
