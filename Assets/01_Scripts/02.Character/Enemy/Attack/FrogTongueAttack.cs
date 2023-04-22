using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrogTongueAttack : EnemyAttack
{
    public override void Attack(Action CallBack)
    {
        //혀 공격 애니메이션 실행시켜줘야함
        callBack = CallBack;
    }

    public void DamageCaster()
    {
        //Damage체크해줘얗ㅁ
        //GameManager.Boxcast()로 체크해줄 예정;
    }
}
