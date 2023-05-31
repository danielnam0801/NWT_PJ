using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 20f;
    

    private void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log(gameObject);
            //collision.GetComponent<IHitable>().GetHit(damage, gameObject);
        }

        //Destroy(gameObject);
    }
}
