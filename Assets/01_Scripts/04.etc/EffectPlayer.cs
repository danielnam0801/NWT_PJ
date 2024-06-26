using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectPlayer : PoolableObject
{
    [SerializeField]
    protected List<ParticleSystem> _particles;

    [SerializeField]
    protected List<VisualEffect> _effects;

    Rigidbody2D rb;

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public void Shoot(float power)
    {
        rb.AddForce(transform.right * power, ForceMode2D.Impulse);
    }

    public void StartPlay(float endTime)
    {
        if (_particles != null)
        {
            _particles.ForEach(p => p.Play());
        }
        if (_effects != null)
        {
            _effects.ForEach(e => e.Play());
        }

        StartCoroutine(Timer(endTime));
    }

    public void StopPlay()
    {
        StopAllCoroutines();
        PoolManager.Instance.Push(this);
    }

    private IEnumerator Timer(float endTime)
    {
        yield return new WaitForSeconds(endTime);
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (_particles != null)
        {
            _particles.ForEach(p => p.Simulate(0));
        }
        if (_effects != null)
        {
            _effects.ForEach(e => e.Stop());
        }
    }

}
