using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltEmissionAttack : EnemyAttack, ISpecialAttack
{
    [SerializeField] GameObject boltPrefab;
    [SerializeField] private float boltSpawnCnt = 7;
    [SerializeField] private float AddForcePower = 7;
    [SerializeField]
    Transform spawnPos;

    public void Attack(Action CallBack)
    {
        this.callBack = CallBack;
        _animator.OnAnimaitionEventTrigger += BoltAttack;
        _animator.OnAnimaitionEndTrigger += AnimationEnd;
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
        float currentAngle = 0f;
        float plusAngle = 180 / boltSpawnCnt - 1;

        float randomAngleRotate = UnityEngine.Random.Range(-15f, 15f);

        for(int i = 0; i < boltSpawnCnt; i++)
        {
            GameObject bolt = Instantiate(boltPrefab, spawnPos.position, Quaternion.Euler(0,0, currentAngle + randomAngleRotate));
            bolt.GetComponent<Rigidbody2D>().AddForce(bolt.transform.right * AddForcePower);
            currentAngle += plusAngle;
        }
    }
}
