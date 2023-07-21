using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemySpawn
{
    public Transform SpawnPos;
    public Enemy SpaenEnemy;
}

public class BattleArea : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    public List<EnemySpawn> enemies = new List<EnemySpawn>();
    [SerializeField]
    private List<Enemy> spawnEnemies = new List<Enemy>();
    public int RespawnNumber;
    [SerializeField]
    private int currentRespawnCount = 1;
    public Transform Wall;

    [SerializeField]
    private bool isOverpast = false;
    [SerializeField]
    private bool isBattle = false;

    private void Start()
    {
        Wall.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOverpast)
            return;

        if(collision.CompareTag("Player"))
        {
            if(!collision.GetComponent<PlayerController>().IsBattle)
                StartBattle();
        }
    }

    private void Update()
    {
        if(isBattle)
        {
            for(int i = 0; i < spawnEnemies.Count; i++)
            {
                if(spawnEnemies[i].IsDead == false)
                {
                    return;
                }
            }

            currentRespawnCount++;

            if (currentRespawnCount >= RespawnNumber)
            {
                FinishBattle();
                return;
            }

            SpawnEenmy();
        }
    }

    private void StartBattle()
    {
        GameManager.instance.Target.GetComponent<PlayerController>().IsBattle = true;
        

        StartCoroutine(SetWall(true, 1f));

        Debug.Log("start battle");
        isBattle = true;
        isOverpast = true;
        DefineETC.VCam.Priority = 50;
        if(cam != null)
        {
            StageManager.Instance.activeCam = cam;
            cam.Priority = 100;
        }
        SpawnEenmy();
    }

    private void FinishBattle()
    {
        isBattle = false;
        StartCoroutine(SetWall(false, 0f));
        DefineETC.VCam.Priority = 100;
        if (cam != null)
        {
            StageManager.Instance.activeCam = null;
            cam.Priority = 0;
        }
        GameManager.instance.Target.GetComponent<PlayerController>().IsBattle = false;
    }

    private void SpawnEenmy()
    {
        for(int i = 0; i < spawnEnemies.Count; i++)
        {
            PoolManager.Instance.Push(spawnEnemies[i]);
        }

        spawnEnemies.Clear();

        for (int i = 0; i < enemies.Count;i++)
        {
            Enemy enemy = PoolManager.Instance.Pop(enemies[i].SpaenEnemy.name) as Enemy;

            enemy.Init();
            enemy.transform.position = enemies[i].SpawnPos.position;
            spawnEnemies.Add(enemy);
        }
    }

    private IEnumerator SetWall(bool value, float time)
    {
        Debug.Log(1);
        yield return new WaitForSeconds(time);

        Wall.gameObject.SetActive(value);
    }
}
