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
    private int currentRespawnCount = 0;

    [SerializeField]
    private bool isOverpast = false;
    [SerializeField]
    private bool isBattle = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOverpast)
            return;

        if(collision.CompareTag("Player"))
        {
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

            if (currentRespawnCount == RespawnNumber)
            {
                FinishBattle();
                return;
            }

            SpawnEenmy();
        }
    }

    private void StartBattle()
    {
        Debug.Log("start battle");
        isBattle = true;
        isOverpast = true;
        DefineETC.VCam.Priority = 0;
        cam.Priority = 100;
        SpawnEenmy();
    }

    private void FinishBattle()
    {
        isBattle = false;
        DefineETC.VCam.Priority = 100;
        cam.Priority = 0;
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

            //if (enemy == spawnEnemies[Mathf.Max(0, i - 1)])
            //{
            //    i--;
            //    continue;
            //}

            enemy.Init();
            enemy.transform.position = enemies[i].SpawnPos.position;
            spawnEnemies.Add(enemy);
        }
    }
}
