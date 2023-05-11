using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum BoltAttackType
{
    RegularShot = 1,
    CollectAndSpreadAttack = 2
}

public class BoltEmissionAttack : EnemyAttack, ISpecialAttack
{
    [SerializeField] GameObject boltPrefab;
    [SerializeField] UnityAction attackFeedbackAction;
    [SerializeField]
    Transform spawnPos;

    [SerializeField] private float boltSpawnCnt = 7;
    [SerializeField] private float AddForcePower = 7;
    [SerializeField] private float CanAttackAngleRange = 360;

    float currentAngle = 0f;
    float plusAngle = 0f;
    float randomAngleRotate = 0f;
    int attackType = 0;
    private int attackTypeCnt = 2;

    List<GameObject> bullet;

    public void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        _animator.OnAnimaitionEventTrigger += BoltAttack;
        _animator.OnAnimaitionEndTrigger += AnimationEnd;

        attackType = UnityEngine.Random.Range(0, attackTypeCnt); //  공격 타입이 늘어날 수록
        plusAngle = CanAttackAngleRange / (boltSpawnCnt - 1);
        randomAngleRotate = UnityEngine.Random.Range(-5f, 5f);

        Debug.LogError("볼트 액션실행중");
    }
    
    private void AnimationEnd()
    {
        Debug.LogError("볼트 액션끝");
        _animator.OnAnimaitionEventTrigger -= BoltAttack;
        _animator.OnAnimaitionEndTrigger -= AnimationEnd;
        CallbackPlay();
    }

    private void BoltAttack()
    {
        if(attackType == 0)
        {
            
        }
        else if(attackType == 1)
        {

        }

        attackFeedbackAction?.Invoke();
        currentAngle = 0f;
        randomAngleRotate = UnityEngine.Random.Range(-5f, 5f);


        for(int i = 0; i < boltSpawnCnt; i++)
        {
            GameObject bolt = Instantiate(boltPrefab, spawnPos.position, Quaternion.Euler(0,0, currentAngle + randomAngleRotate));
            bolt.GetComponent<Rigidbody2D>().AddForce(bolt.transform.right * AddForcePower, ForceMode2D.Impulse);
            currentAngle += plusAngle;
        }
    }

    
    private void CollectAndSpreadAttack()
    {
        GameObject bolt = Instantiate(boltPrefab, spawnPos.position, Quaternion.Euler(0,0, currentAngle + randomAngleRotate));
        bullet.Add(bolt);
        currentAngle += plusAngle;
    }
}
