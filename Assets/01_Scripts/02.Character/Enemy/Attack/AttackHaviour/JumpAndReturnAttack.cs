using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAndReturnAttack : EnemyAttack, IRangeAttack
{
    [SerializeField] EnergyBall EnergyBallPrefab;
    [SerializeField] float damage = 20f;
    [SerializeField] Transform spawnPos;
    public void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        StartCoroutine(JumpAttackAndReturn());
        Debug.LogError("점프앤드 리턴 실행중");
        _animator.SetAnimatorSpeed(0);
    }


    IEnumerator JumpAttackAndReturn()
    {
        EnergyBall energyball = PoolManager.Instance.Pop(EnergyBallPrefab.gameObject.name) as EnergyBall;
        energyball.transform.position = spawnPos.position;
        energyball.SetValueAndPlay(damage, _brain.Target);
        while (energyball.gameObject.activeSelf)
        {
            if (energyball.isShootReady)
            {
                energyball.isShootReady = false;
                _animator.SetAnimatorSpeed(1);
            }
            yield return null;  
        }
        CallbackPlay();
    }


}
