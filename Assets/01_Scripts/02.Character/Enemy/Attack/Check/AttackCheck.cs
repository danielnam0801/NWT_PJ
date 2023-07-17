using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [Tooltip("(충돌 처리 할 것들은 Physics세팅 건들어주셈)")]
    [SerializeField] private AttackCheckType _attackCheckType;
    [SerializeField] float damage = 5f;
    [SerializeField] Transform centerPos;

    [Space(20)]
    [Header("Circle")]
    [SerializeField]
    float radius;

    [Header("Box")]
    [SerializeField]
    Vector2 size;

    [Header("Line")]
    [SerializeField]
    float lineLength = 5f;

    Collider2D colider;
    PoolableObject poolObj;

    private bool collisionCheck;
    private bool colliderCheck;
    private bool rayCheck;
    private bool overlapCheck;

    private bool circle;
    private bool box;
    private bool line;

    private void OnEnable()
    {
        poolObj = transform.GetComponent<PoolableObject>();
    }

    private void Start()
    {
        centerPos = transform;
        switch (_attackCheckType)
        {
            case AttackCheckType.COLLISION:
                collisionCheck = true;
                break;
            case AttackCheckType.COLLIDER:
                colliderCheck = true;
                break;
            case AttackCheckType.CIRCLE_OVERLAP:
                circle = true;
                overlapCheck = true;
                break;
            case AttackCheckType.BOX_OVERLAP:
                box = true;
                overlapCheck = true;
                break;
            case AttackCheckType.LINE_RAYCAST:
                line = true;
                rayCheck = true;
                break;
            case AttackCheckType.BOX_RAYCAST:
                box = true;
                rayCheck = true;
                break;
            case AttackCheckType.CIRCLE_RAYCAST:
                circle = true;
                rayCheck = true;
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionCheck)
        {
            IHitable hitable;
            if(collision.gameObject.TryGetComponent<IHitable>(out hitable)){

                hitable.GetHit(damage, this.gameObject,
                    (collision.transform.position - transform.position).normalized);
                //PoolManager.Instance.Push(poolObj);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colliderCheck)
        {
            IHitable hitable;
            if (collision.gameObject.TryGetComponent<IHitable>(out hitable))
            {
                hitable.GetHit(damage, this.gameObject,
                    (collision.transform.position - transform.position).normalized);
            }
            //PoolManager.Instance.Push(poolObj);
        }
    }

    private void Update()
    {
        if(overlapCheck) OverlapCheck();
        if(rayCheck) RayCheck();
    }

    public void OverlapCheck()
    {
        if (circle)
        {
            Collider2D[] circleHits = Physics2D.OverlapCircleAll(centerPos.position, radius);
            for (int i = 0; i < circleHits.Length; i++)
            {
                IHitable hitable;
                if (circleHits[i].TryGetComponent<IHitable>(out hitable))
                {
                    hitable.GetHit(damage, this.gameObject,
                        (circleHits[i].transform.position - transform.position).normalized);
                }
            }
        }
        if (box)
        {
            Collider2D[] boxHits = Physics2D.OverlapBoxAll(centerPos.position, size, 0);
            for (int i = 0; i < boxHits.Length; i++)
            {
                IHitable hitable;
                if (boxHits[i].TryGetComponent<IHitable>(out hitable))
                {
                    hitable.GetHit(damage, this.gameObject,
                        (boxHits[i].transform.position - transform.position).normalized);
                }
            }
        }
    }

    public void RayCheck()
    {
        if(line){
            RaycastHit2D[] hit = Physics2D.RaycastAll(centerPos.position, transform.right, lineLength);
            if (hit.Length > 0)
            {
                foreach(var a in hit)
                {
                    IHitable hitable;
                    if(a.collider.TryGetComponent<IHitable>(out hitable)){
                        hitable.GetHit(damage, this.gameObject, a.normal);
                    }
                }
            }
        }
        if (circle)
        {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(centerPos.position, radius, transform.right);
            if (hit.Length > 0)
            {
                foreach (var a in hit)
                {
                    IHitable hitable;
                    if (a.collider.TryGetComponent<IHitable>(out hitable))
                    {
                        hitable.GetHit(damage, this.gameObject, a.normal);
                    }
                }
            }
        }
        if (box)
        {
            RaycastHit2D[] hit = Physics2D.BoxCastAll(centerPos.position, size, 0, transform.right);
            if (hit.Length > 0)
            {
                foreach (var a in hit)
                {
                    IHitable hitable;
                    if (a.collider.TryGetComponent<IHitable>(out hitable))
                    {
                        hitable.GetHit(damage, this.gameObject, a.normal);
                    }
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(centerPos.position, size);
    }

}