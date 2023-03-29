using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack(float damage)
    {
        if (_waitBeforeNextAttack == false)
        {
            _brain.AIActionData.isAttack = true;
            StartCoroutine("Wait",damage);
            StartCoroutine(WaitBeforeAttackCoroutine());
            
        }
    }


    IEnumerator Wait(int damage)
    {
        float range = _brain.Enemy.EnemyData.AttackRange;

        yield return new WaitForSeconds(0.3f);
        float distance = Vector2.Distance(transform.position, _brain.Target.position);

        if (distance < range)
        {
            //PlayerHP playerHP = GameManager.instance.Target.gameObject.GetComponent<PlayerHP>();
            //playerHP.Damage(damage);
        }
    }
}

