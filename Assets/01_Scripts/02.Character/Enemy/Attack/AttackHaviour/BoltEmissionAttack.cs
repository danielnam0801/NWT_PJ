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
    [SerializeField] GameObject boltPrefab;
    [SerializeField] GameObject linePrefab;
    [SerializeField] GameObject electroSphere;
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
    List<GameObject> bullet = new List<GameObject>();
    int currentSpawnBulletCnt = 0;


    [Header("ElectoBall")]
    [SerializeField] GameObject portal;
    [SerializeField] private float teleportPlayTIme = 2f;
    private Action AppearAction;
    private Action DisappearAction;


    public void Attack(Action CallBack)
    {
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
            GameObject bolt = Instantiate(boltPrefab, spawnPos.position, Quaternion.Euler(0, 0, currentAngle + randomAngleRotate));
            bolt.GetComponent<Rigidbody2D>().AddForce(bolt.transform.right * AddForcePower, ForceMode2D.Impulse);
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
            
            GameObject bolt = Instantiate(boltPrefab, spawnPos.position, Quaternion.Euler(0, 0, currentAngle + randomAngleRotate));
            bolt.transform.position += bolt.transform.right * 2;
            bullet.Add(bolt);
            currentAngle += plusAngle;
            yield return new WaitForSeconds(animPlayingTime/ boltSpawnCnt);
        }

        yield return new WaitForSeconds(0.05f);
        bullet.ForEach((enemyBullet) =>
        {
            enemyBullet.GetComponent<Rigidbody2D>().AddForce(enemyBullet.transform.right * AddForcePower * 3, ForceMode2D.Impulse);
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
        GameObject electro = Instantiate(electroSphere, spawnPos.position, Quaternion.identity);
        _animator.Animator.SetFloat("AttackSpeed", animSpeed);
        for(int i = 0; i < electroLineSpawnCnt; i++)
        {
            GameObject line1 = Instantiate(linePrefab, spawnPos.position, Quaternion.Euler(-currentAngle, 90, 0));
            line1.transform.position += line1.transform.right * 2;
            GameObject line2 = Instantiate(linePrefab, spawnPos.position, Quaternion.Euler(-(CanAttackAngleRange - currentAngle), 90, 0));
            line2.transform.position += line2.transform.right * 2;

            //LightningBoltScript _line1 = line1.GetComponent<LightningBoltScript>(); 
            //LightningBoltScript _line2 = line2.GetComponent<LightningBoltScript>();

            RaycastHit2D ray1 = Physics2D.Raycast(line1.transform.position, line1.transform.right, 10, isGround);
            RaycastHit2D ray2 = Physics2D.Raycast(line2.transform.position, line2.transform.right, 10, isGround);

            //_line1.StartPosition = line1.transform.position - line1.transform.position;
            //_line2.StartPosition = line2.transform.position - line2.transform.position;

            ////if (ray1.collider != null) _line1.EndPosition = ray1.point;
            //_line1.EndPosition = line1.transform.position + (line1.transform.right * 10) - line1.transform.position;
            ////if (ray2.collider != null) _line2.EndPosition = ray2.point;
            //_line2.EndPosition = line2.transform.position + (line2.transform.right * 10) - line2.transform.position;
            
            currentAngle += plusAngle;

            Destroy(line1, destroyTime);
            Destroy(line2, destroyTime);

            yield return new WaitForSeconds(animPlayingTime * (1/animSpeed) / electroLineSpawnCnt);
        }
        _animator.Animator.SetFloat("AttackSpeed", 1);
        Destroy(electro);
    }
}
