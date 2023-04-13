using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongueAttack : EnemyAttack
{   
    public override void Attack(Action CallBack)
    {
        //�� ���� �ִϸ��̼� ������������;
        StartCoroutine(AttackDamageDelayCoroutine(PlayTime, DamageCaster));
    }

    void DamageCaster()
    {       
        //Damageüũ����餱
        _stateInfo.IsFrogTongueAttack = false;
    }
}
