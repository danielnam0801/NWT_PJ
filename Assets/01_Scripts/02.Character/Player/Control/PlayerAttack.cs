using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerWeapon weapon;
    public PlayerWeapon Weapon { get => weapon; set => weapon = value; }
    public LayerMask EnemyLayer;
    private void Start()
    {
        weapon = FindObjectOfType<PlayerWeapon>();
    }

    public void CreateShpae(ShapeType shape)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, EnemyLayer);

        if(enemies.Length > 0)
        {
            Transform closestEnemy = enemies[0].transform;

            if(enemies.Length > 1)
            {
                Vector2 position = transform.position;
                float minDistance = Vector2.Distance(position, closestEnemy.position);    

                for (int i = 1; i < enemies.Length; i++)
                {
                    if (Vector2.Distance(position, enemies[i].transform.position) < minDistance)
                        closestEnemy = enemies[i].transform;
                }
            }


            GuideLine obj = PoolManager.Instance.Pop($"{shape}GuideLine") as GuideLine;
            obj.SetPair(closestEnemy.transform.Find("GuidePosition"));
        }
    }
}
