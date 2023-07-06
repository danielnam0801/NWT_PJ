using DigitalRuby.LightningBolt;
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
    [SerializeField] EffectPlayer boltPrefab;
    [SerializeField] EffectPlayer linePrefab;
    [SerializeField] EffectPlayer electroSphere;
    [SerializeField] UnityAction attackFeedbackAction;
    [SerializeField]
    Transform spawnPos;

    [SerializeField] private float boltSpawnCnt = 7;
    [SerializeField] private float electroLineSpawnCnt = 5; 
    [SerializeField] private float AddForcePower = 7;
    [SerializeField] private float CanAttackAngleRange = 180;

    float currentAngle = 0f;
    float plusAngle = 0f;
    float randomAngleRotate = 0f;
    int attackType = 0;
    
    private int attackTypeCnt = 2;
    [SerializeField]
    LayerMask isGround;

    // 모아서 쏘는 공격용 변수
    List<EffectPlayer> bullet = new List<EffectPlayer>();
    int currentSpawnBulletCnt = 0;


    [Header("ElectoBall")]
    [SerializeField] GameObject portal;
    [SerializeField] private float teleportPlayTIme = 2f;
    private Action AppearAction;
    private Action DisappearAction;


    public void Attack(Action CallBack)
    {
        SetAnimAttack();
        this.callBack = CallBack;
        Init();
        _animator.OnAnimaitionEventTrigger += BoltAttack;
        _animator.OnAnimaitionEndTrigger += AnimationEnd;

        attackType = UnityEngine.Random.Range(0, attackTypeCnt); //  공격 타입이 늘어날 수록
        
        randomAngleRotate = UnityEngine.Random.Range(-5f, 5f);

        Debug.LogError("볼트 액션실행중");
    }

    private void Init()
    {
        foreach(var a in bullet)
        {
            if (bullet != null) PoolManager.Instance.Push(a);
        }
        bullet.Clear();
        currentSpawnBulletCnt = 0;
        currentAngle = 0;
        _animator.Animator.SetFloat("AttackSpeed", 1);
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
        attackFeedbackAction?.Invoke();
        randomAngleRotate = UnityEngine.Random.Range(-5f, 5f);
        //attackType = 1;
        if (attackType == 0)
        {
            plusAngle = CanAttackAngleRange / (boltSpawnCnt - 1);
            CollectAndSpreadAttack();
        }
        else if (attackType == 1)
        {
            plusAngle = CanAttackAngleRange / (electroLineSpawnCnt - 1);
            LightingAttack();
        }
    }

    private void TeleportAttack()
    {
        SpawnPortal(transform.position);
        DisappearAction?.Invoke();

        _animator.Animator.SetFloat("AttackSpeed", 0.5f);
        StartCoroutine(DelayCoroutine(1.5f, Teleporting));
    }


    private void Teleporting()
    {
        _animator.Animator.SetFloat("AttackSpeed", 1f);
        AppearAction?.Invoke();
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-1f,1f), 0, 0);
        Vector3 SpawnPos = _brain.Target.position + randomPosition * UnityEngine.Random.Range(1f,2f);
        _brain.transform.position = SpawnPos;
    }

    private void SpawnPortal(Vector3 position)
    {
        Instantiate(portal, position, Quaternion.identity);
    }

    private void BoltNormalAttack()
    {
        currentAngle = 0f;
        for (int i = 0; i < boltSpawnCnt; i++)
        {
            EffectPlayer bolt = PoolManager.Instance.Pop(boltPrefab.gameObject.name) as EffectPlayer;
            bolt.SetPositionAndRotation(spawnPos.position, Quaternion.Euler(0, 0, currentAngle + randomAngleRotate));
            bolt.Shoot(AddForcePower);
            currentAngle += plusAngle;
        }
    }

    private void CollectAndSpreadAttack()
    {
        StartCoroutine(nameof(SpawnBullet));
    }

    float animPlayingTime = 1.15f;
    IEnumerator SpawnBullet()
    {   
        for(int i = 0; i < boltSpawnCnt; i++)
        {
            EffectPlayer bolt = PoolManager.Instance.Pop(boltPrefab.gameObject.name) as EffectPlayer;
            bolt.SetPositionAndRotation(spawnPos.position, Quaternion.Euler(0, 0, currentAngle + randomAngleRotate));
            bolt.transform.position += bolt.transform.right * 2;
            bolt.StartPlay(5f);
            bullet.Add(bolt);
            currentAngle += plusAngle;
            yield return new WaitForSeconds(animPlayingTime/ boltSpawnCnt);
        }

        yield return new WaitForSeconds(0.05f);
        bullet.ForEach((enemyBullet) =>
        {
            enemyBullet.Shoot(AddForcePower);
        });
    }
    private void LightingAttack()
    {
        StartCoroutine(LineSpawn());
    }

    private float animSpeed = 0.5f;
    private float destroyTime = 0.5f;
    private IEnumerator LineSpawn()
    {
        EffectPlayer electro = PoolManager.Instance.Pop(electroSphere.gameObject.name) as EffectPlayer;
        electro.SetPositionAndRotation(spawnPos.position, Quaternion.identity);
        electro.StartPlay(5f);

        _animator.Animator.SetFloat("AttackSpeed", animSpeed);
        for(int i = 0; i < electroLineSpawnCnt; i++)
        {
            //Debug.LogError(linePrefab.gameObject.name);
            EffectPlayer line1 = PoolManager.Instance.Pop(linePrefab.gameObject.name) as EffectPlayer;
            
            line1.SetPositionAndRotation(spawnPos.position, Quaternion.Euler(0, 0, currentAngle));
            line1.transform.position += line1.transform.right / 2;
            line1.StartPlay(animPlayingTime * (1 / animSpeed) / electroLineSpawnCnt + 0.3f);

            EffectPlayer line2 = PoolManager.Instance.Pop(linePrefab.gameObject.name) as EffectPlayer;
            
            line2.SetPositionAndRotation(spawnPos.position, Quaternion.Euler(0, 0, (CanAttackAngleRange - currentAngle)));
            line2.transform.position += line2.transform.right / 2;
            line2.StartPlay(animPlayingTime * (1 / animSpeed) / electroLineSpawnCnt + 0.3f);

            currentAngle += plusAngle;

            StartCoroutine(DelayCoroutine(destroyTime, () => PoolManager.Instance.Push(line1)));
            StartCoroutine(DelayCoroutine(destroyTime, () => PoolManager.Instance.Push(line2)));

            yield return new WaitForSeconds(animPlayingTime * (1/animSpeed) / electroLineSpawnCnt);
        }
        _animator.Animator.SetFloat("AttackSpeed", 1);
        electro.StopPlay();
        PoolManager.Instance.Push(electro);
    }
}
