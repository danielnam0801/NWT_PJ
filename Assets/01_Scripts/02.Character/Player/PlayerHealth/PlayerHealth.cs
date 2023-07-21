using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IHitable
{
    public bool IsEnemy => false;
    public float UnHitTime = 0.5f;
    private bool unHit = false;

    public Vector3 HitPoint { get; set; }

    public float hp = 0;
    public float maxHp = 100;

    public UnityEvent<float> GetHitEvent;
    public UnityEvent DieEvent;

    private void Start()
    {
        hp = maxHp;
    }

    public void GetHit(float damage, GameObject damageDealer, Vector3 HitNormal)
    {
        if (unHit) 
            return;

        StartCoroutine(UnHitCoroutine(UnHitTime));
        hp -= damage;

        hp = Mathf.Clamp(hp, 0, maxHp);

        GetHitEvent?.Invoke(hp / maxHp);

        if (hp <= 0)
        {
            Die();
        }
    }

    private IEnumerator UnHitCoroutine(float time)
    {
        unHit = true;

        yield return new WaitForSeconds(time);

        unHit = false;
    }
        

    private void Die()
    {
        //Destroy(gameObject);

        TimeManager.Instance.SetTimeScale(0.2f);
        DieEvent?.Invoke();
    }
}
