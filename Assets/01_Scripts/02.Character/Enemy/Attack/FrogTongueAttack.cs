using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrogTongueAttack : EnemyAttack
{
    public override void Attack(Action CallBack)
    {
        //�� ���� �ִϸ��̼� ������������
        callBack = CallBack;
    }

    public void DamageCaster()
    {
        //Damageüũ����餱
        //GameManager.Boxcast()�� üũ���� ����;
    }
}
