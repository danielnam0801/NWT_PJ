using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] float damage = 5f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("ISHIt");
            IHitable hitable;
            if (collision.transform.TryGetComponent<IHitable>(out hitable))
            {
                hitable.GetHit(damage, this.gameObject);
            }
        }
    }
}
