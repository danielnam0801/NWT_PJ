using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDamage : MonoBehaviour
{
    [SerializeField] float radius;
    Vector3 hitPoint;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hitPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D[] collider = Physics2D.OverlapCircleAll(hitPoint, radius, 1 << LayerMask.NameToLayer("Enemy"));

            foreach(Collider2D col in collider)
            {
                IHitable hitable;
                if (col.TryGetComponent<IHitable>(out hitable))
                {
                    hitable.GetHit(3, this.gameObject);
                }
            }
        }
    }
}
