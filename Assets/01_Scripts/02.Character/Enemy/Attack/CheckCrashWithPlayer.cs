using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCrashWithPlayer : MonoBehaviour
{
    [SerializeField] float damage = 3f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hitable = null;
        if (collision.gameObject.TryGetComponent<IHitable>(out hitable))
        {
            hitable.GetHit(damage, this.gameObject);
        }
    }
}
