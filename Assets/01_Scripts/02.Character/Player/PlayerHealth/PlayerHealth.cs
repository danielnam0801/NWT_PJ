using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHitable
{
    public bool IsEnemy => false;

    public Vector3 HitPoint { get; set; }

    public float hp = 0;
    public float maxHp = 100;

    private void Start()
    {
        hp = maxHp;
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        hp -= damage;

        hp = Mathf.Clamp(hp, 0, maxHp);

        Debug.LogError("HITTTTTTTTTTTT");
        if(hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
