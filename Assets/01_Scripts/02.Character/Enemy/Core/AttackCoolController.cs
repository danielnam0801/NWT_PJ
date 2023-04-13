using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AttackCoolController : MonoBehaviour
{
    Enemy _enemy;
    EnemyMovement _movement;
    Dictionary<SkillName, float> attackCoolList;
    Dictionary<SkillName, bool> endCoolAttackList; //��Ÿ�� ���� ��ų���� true����
    Dictionary<SkillName, EnemyAttack> attackActionList;
    AIStateInfo _stateInfo;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _enemy = GetComponent<Enemy>();
        _movement = GetComponent<EnemyMovement>();
        _stateInfo = transform.Find("AttackType").GetComponent<AIStateInfo>();
        attackCoolList = new Dictionary<SkillName, float>();
        endCoolAttackList = new Dictionary<SkillName, bool>();
        attackActionList = new Dictionary<SkillName, EnemyAttack>();

        _enemy.EnemyData._EnemyAttackDatas.ForEach((enemy) =>
        {
            attackCoolList.Add(enemy.AttackName, enemy.coolTime);
            endCoolAttackList.Add(enemy.AttackName, false);
            attackActionList.Add(enemy.AttackName, enemy.atk);
        });

    
    }

    Tuple<float,float> SearchSkillData(SkillName skillname)
    {
        foreach(var enemy in _enemy.EnemyData._EnemyAttackDatas)
        {
            if (enemy.AttackName == skillname)
            {
                float damage = enemy.damage;
                float coolTime = enemy.coolTime;
                return new Tuple<float, float>(damage, coolTime) ;
            }
        }
        Debug.LogError("Skill�̸��� �ٸ��� �Ǿ����� : " + skillname);
        return new Tuple<float,float>(0,0);
    }

    public virtual void Attack(SkillName skillname)
    {
        if (_stateInfo.IsAttack)
        {
            return;
        }

        endCoolAttackList[skillname] = isCoolDown(skillname);
        if(endCoolAttackList[skillname] == true)
        {
            Tuple<float, float> skillData = SearchSkillData(skillname);
            float damage = skillData.Item1; // skill�� damage�� �ٸ����� ã����
            float cooltime = skillData.Item2;

            FieldInfo fInfoBool = typeof(AIStateInfo)
            .GetField($"Is{skillname.ToString()}", BindingFlags.Public | BindingFlags.Instance);
           
            _movement.StopImmediatelly();
            _stateInfo.IsAttack = true;
            fInfoBool.SetValue(_stateInfo, true);
            attackActionList[skillname].Attack(damage);
            SetCoolDown(skillname, cooltime);
        }
        else
        {
            Debug.Log($"{skillname} ���� ��Ÿ�� �����");
        }
    }

    public bool isCoolDown(SkillName key)
    {
        float coolDown;
        if (attackCoolList.TryGetValue(key, out coolDown))
        {
            return Time.time > coolDown; // ��Ÿ�� �� ������ true
        }
        else
        {
            return false;
        }
    }

    public void SetCoolDown(SkillName key, float duration)
    {
        float coolDown = Time.time + duration;
        if (attackCoolList.ContainsKey(key))
        {
            attackCoolList[key] = coolDown;
        }
        else
        {
            attackCoolList.Add(key, coolDown);
        }
    }

}
