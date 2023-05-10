using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    [SerializeField]
    private WeaponSO info;
    public float rotateTime = 1;

    protected Transform playerSwordTrm;

    public bool IsFollow { get; set; }
    public bool IsStay { get; set; }
    private bool isAttack = false;
    public WeaponSO Info
    {
        get { return info; }
        private set { }
    }

    protected virtual void Awake()
    {
        IsFollow = true;
        IsStay = false;
        playerSwordTrm = GameObject.Find("Player/SwordPosition").transform;
    }

    protected virtual void Start()
    {
        LightManager.Instance.AddFocusObject(gameObject);
    }

    protected virtual void Update()
    {
        MoveToPlayer();
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttack)
            return;
        if (collision.gameObject.CompareTag("Player"))
            return;

        if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hit))
        {
            hit.GetHit(info.power, gameObject);
            Debug.Log(collision.name);
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
    }

    public IEnumerator Attack(List<Vector2> pathPoints)
    {
        isAttack = true;
        IsFollow = false;
        transform.position = pathPoints[0];

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            float currentMoveTime = 0;

            Rotate(pathPoints[i + 1]);

            while(currentMoveTime < 1)
            {
                currentMoveTime += Time.deltaTime / info.pointUnitMoveTime;
                transform.position = Vector2.Lerp(pathPoints[i], pathPoints[i + 1], currentMoveTime);

                yield return null;
            }
        }

        isAttack = false;
        StartCoroutine("Stay");
    }

    private IEnumerator Stay()
    {
        IsStay = true;
        IsFollow = false;

        yield return new WaitForSeconds(info.stayTime);

        IsStay = false;
        IsFollow = true;
        DrawManager.Instance.SetDelayDraw(Info.attackDelayTime);
        StartCoroutine(RotateVertical());
    }

    public void StopStay()
    {
        StopCoroutine("Stay");
        IsStay = false;
        IsFollow = true;
        StartCoroutine(RotateVertical());
    }

    private void Rotate(Vector2 targetDir)
    {
        Vector2 dir = (targetDir - (Vector2)transform.position).normalized;

        transform.right = Quaternion.Euler(0, 0, -135f) * dir;
    }

    private IEnumerator RotateVertical()
    {
        Vector2 originRight = transform.right; 
        Vector2 dir = Vector2.up;

        float current = 0;
        float percent = 0;
        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / rotateTime;

            transform.right = Vector2.Lerp(originRight, Quaternion.Euler(0, 0, -135f) * dir, percent);
            yield return null;  
        }
    }
}
