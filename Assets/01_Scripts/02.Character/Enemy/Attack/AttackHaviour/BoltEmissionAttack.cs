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
    
    [SerializeField]
    private int attackTypeCnt = 2;

    // 모아서 쏘는 공격용 변수
    List<GameObject> bullet = new List<GameObject>();
    int currentSpawnBulletCnt = 0;
    int callMaxValue = 5;

    public void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        Init();
        _animator.OnAnimaitionEventTrigger += BoltAttack;
        _animator.OnAnimaitionEndTrigger += AnimationEnd;

        attackType = UnityEngine.Random.Range(0, attackTypeCnt); //  공격 타입이 늘어날 수록
        plusAngle = CanAttackAngleRange / (boltSpawnCnt - 1);
        randomAngleRotate = UnityEngine.Random.Range(-5f, 5f);

        Debug.LogError("볼트 액션실행중");
    }

    private void Init()
    {
        bullet.Clear();
        currentSpawnBulletCnt = 0;
        currentAngle = 0;
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
        currentSpawnBulletCnt++;
        attackFeedbackAction?.Invoke();
        randomAngleRotate = UnityEngine.Random.Range(-5f, 5f);
        
        if (attackType == 0)
        {
            //CollectAndSpreadAttack();
            BoltNormalAttack();
        }
        else if (attackType == 1)
        {
            //BoltNormalAttack();
            CollectAndSpreadAttack();
        }

    }

    private void BoltNormalAttack()
    {
        currentAngle = 0f;
        for (int i = 0; i < boltSpawnCnt; i++)
        {
            GameObject bolt = Instantiate(boltPrefab, spawnPos.position, Quaternion.Euler(0, 0, currentAngle + randomAngleRotate));
            bolt.GetComponent<Rigidbody2D>().AddForce(bolt.transform.right * AddForcePower, ForceMode2D.Impulse);
            currentAngle += plusAngle;
        }
    }

    private void CollectAndSpreadAttack()
    {
        if(currentSpawnBulletCnt >= callMaxValue)
        {
            bullet.ForEach((enemyBullet) =>
            {
                enemyBullet.GetComponent<Rigidbody2D>().AddForce(enemyBullet.transform.right * AddForcePower * 3, ForceMode2D.Impulse);
            });
            return;
        }

        Debug.Log("CurrentAngle : " + currentAngle);
        GameObject bolt = Instantiate(boltPrefab, spawnPos.position, Quaternion.Euler(0,0, currentAngle + randomAngleRotate));
        bolt.transform.position += bolt.transform.right * 2;
        bullet.Add(bolt);
        currentAngle += plusAngle;
    }

    private void Lightningattack()
    {

    }
}
