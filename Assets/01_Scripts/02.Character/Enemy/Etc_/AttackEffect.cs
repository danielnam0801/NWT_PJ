using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : PoolableObject
{
    [SerializeField]
    private float moveSpeed = 10;
    [SerializeField]
    private float lifeTime = 105f;

    private void Update()
    {
        transform.Translate(Quaternion.identity * (Vector2.right * moveSpeed * Time.deltaTime));
    }

    public override void Init()
    {
        Transform sword = GameManager.instance.Target.GetComponent<PlayerAttack>().Weapon.transform;
        transform.position = sword.position;
        transform.right = Quaternion.Euler(0, 0, -225f) * sword.right;

        StartCoroutine(Push());
    }

    private IEnumerator Push()
    {
        yield return new WaitForSeconds(lifeTime);

        PoolManager.Instance.Push(this);
    }
}
