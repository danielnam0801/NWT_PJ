using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShootAttack : EnemyAttack
{
    [SerializeField] GameObject enemyBullet;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootingPower = 5f;
    public override void Attack(float damage)
    {
        Debug.Log("AttackSS");
        if (_waitBeforeNextAttack == false)
        {
            Debug.Log("Attack");
            _brain.AIActionData.isAttack = true;
            Shooting();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }

    private void Shooting()
    {
        Debug.Log("Shoot");
        Vector2 dir = _brain.Target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject Bu = Instantiate(enemyBullet, shootPoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Bu.GetComponent<Rigidbody2D>().AddForce(dir.normalized * shootingPower, ForceMode2D.Impulse);
    }
}
