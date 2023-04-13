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
    Dictionary<SkillName, EnemyAttackData> _attackDictionary;
    AIStateInfo _stateInfo;

    private void Awake()
    {
        Init();
        MakeAttackTypeAction();
    }

    private void Init()
    {
        _enemy = GetComponent<Enemy>();
        _movement = GetComponent<EnemyMovement>();
        _stateInfo = transform.Find("AI").GetComponent<AIStateInfo>();
        attackCoolList = new Dictionary<SkillName, float>();
        _attackDictionary = new Dictionary<SkillName, EnemyAttackData>();
    }

    private void MakeAttackTypeAction()
    {
        Transform atkTrm = transform.Find("AttackType");
        EnemyAttackData jumpAttack = new EnemyAttackData()
        {
            atk = atkTrm.GetComponent<JumpAttack>(),
            AttackName = SkillName.FrogJumpAttack,
            action = () => {
                _stateInfo.IsFrogJumpAttack = false;
                _stateInfo.IsAttack = false;
            },
            coolTime = 3f,
            damage = 1
        };
        EnemyAttackData tongueAttack = new EnemyAttackData()
        {
            atk = atkTrm.GetComponent<JumpAttack>(),
            AttackName = SkillName.FrogTongueAttack,
            action = () => {
                _stateInfo.IsFrogTongueAttack = false;
                _stateInfo.IsAttack = false;
            },
            coolTime = 2f,
            damage = 1
        };

        _attackDictionary.Add(SkillName.FrogJumpAttack, jumpAttack);
        _attackDictionary.Add(SkillName.FrogTongueAttack, tongueAttack);
        

    }

    public virtual void Attack(SkillName skillname)
    {
        if (_stateInfo.IsAttack)
        {
            return;
        }

        if (!isCoolDown(skillname)) return;
        else
        {
            FieldInfo fInfoBool = typeof(AIStateInfo)
            .GetField($"Is{skillname.ToString()}", BindingFlags.Public | BindingFlags.Instance);
            
            EnemyAttackData atkData = null;
            if(_attackDictionary.TryGetValue(skillname, out atkData)){
                _movement.StopImmediatelly();
                _stateInfo.IsAttack = true;
                fInfoBool.SetValue(_stateInfo, true);
                attackCoolList[skillname] = atkData.coolTime;
                atkData.atk.Attack(atkData.action);
                SetCoolDown(skillname, atkData.coolTime);
            }
        }
    }

    public bool isCoolDown(SkillName key)
    {
        float coolDown;
        if (attackCoolList.TryGetValue(key, out coolDown))
        {
            return Time.time > coolDown; // 쿨타임 다 지나면 true
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
