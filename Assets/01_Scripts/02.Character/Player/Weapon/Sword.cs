using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hit;
        Debug.Log(collision.name);

        if (collision.gameObject.TryGetComponent<IHitable>(out hit))
        {
            hit.GetHit(damage, gameObject);
            
        }
    }
}
