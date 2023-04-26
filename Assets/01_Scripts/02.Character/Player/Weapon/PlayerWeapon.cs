using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public bool IsFollow { get; set; }
    public bool IsStay { get; set; }

    [SerializeField]
    private WeaponSO info;

    protected Transform playerSwordTrm;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        IsFollow = true;
        IsStay = false;
        playerSwordTrm = GameObject.Find("Player/SwordPosition").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        MoveToPlayer();
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hit))
        {
            hit.GetHit(info.power, gameObject);
        }
    }

    private void MoveToPlayer()
    {
        if (!IsFollow)
            return;

        if(Vector2.Distance(transform.position, playerSwordTrm.position) >= info.followMinDistance)
        {
            transform.position = Vector2.Lerp(transform.position, playerSwordTrm.position, Time.deltaTime * info.followSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public IEnumerator Attack(List<Vector2> pathPoints)
    {
        IsFollow = false;
        transform.position = pathPoints[0];

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            float currentMoveTime = 0;

            while(currentMoveTime < 1)
            {
                currentMoveTime += Time.deltaTime / info.pointUnitMoveTime;
                transform.position = Vector2.Lerp(pathPoints[i], pathPoints[i + 1], currentMoveTime);

                yield return null;
            }
        }

        StartCoroutine("Stay");
    }

    private IEnumerator Stay()
    {
        IsStay = true;
        IsFollow = false;

        yield return new WaitForSeconds(info.stayTime);

        IsStay = false;
        IsFollow = true;
    }

    public void StopStay()
    {
        StopCoroutine("Stay");
        IsStay = false;
        IsFollow = true;
    }
}
