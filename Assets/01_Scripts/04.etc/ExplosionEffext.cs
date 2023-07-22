using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffext : PoolableObject
{
    private ParticleSystem pc;

    private void Awake()
    {
        pc = GetComponent<ParticleSystem>();
    }

    public override void Init()
    {
        pc.Play();

        StartCoroutine(Push());
    }

    private IEnumerator Push()
    {
        while(pc.isPlaying)
        {
            yield return null;
        }

        PoolManager.Instance.Push(this);
    }
}
