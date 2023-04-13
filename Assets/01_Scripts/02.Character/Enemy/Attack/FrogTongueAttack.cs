using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongueAttack : EnemyAttack
{   
    public override void Attack(float damage)
    {
        //혀 공격 애니메이션 실행시켜줘야함;
        StartCoroutine(AttackDamageDelayCoroutine(PlayTime, DamageCaster));
    }

    void DamageCaster()
    {       
        //Damage체크해줘얗ㅁ
        _stateInfo.IsFrogTongueAttack = false;
    }
}
