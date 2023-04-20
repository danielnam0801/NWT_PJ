using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    protected float damage;
    protected float attackDelay;
    protected float moveSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hit;
        Debug.Log(collision.name);

        if (collision.gameObject.TryGetComponent<IHitable>(out hit))
        {
            hit.GetHit(damage, gameObject);
        }
    }

    private void Move()
    {

    }

    protected virtual void SetRotation()
    {

    }

    public void SetRotation(Vector2 dir)
    {
        transform.up = -dir;
    }
}
