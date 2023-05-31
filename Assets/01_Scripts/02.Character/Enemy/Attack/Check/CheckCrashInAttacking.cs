using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCrashInAttacking : MonoBehaviour
{
    [SerializeField] float damage = 3f;
    AIStateInfo _aiStateInfo;
    private void Awake()
    {
        _aiStateInfo = transform.Find("AI").GetComponent<AIStateInfo>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_aiStateInfo.IsAttack)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("ISHIt");
                IHitable hitable;
                if(collision.transform.TryGetComponent<IHitable>(out hitable))
                {
                    hitable.GetHit(damage, this.gameObject);
                }
                _aiStateInfo.IsCrash = true;
            }
        }
    }
}
