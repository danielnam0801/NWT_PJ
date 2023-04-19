using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AttackCoolController : MonoBehaviour
{
    Enemy _enemy;
    EnemyMovement _movement;
    Dictionary<SkillName, float> _attackCoolList;
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
        _attackCoolList = new Dictionary<SkillName, float>();
        _attackDictionary = new Dictionary<SkillName, EnemyAttackData>();
    }

    private void MakeAttackTypeAction()
    {
        Transform atkTrm = transform.Find("AttackType");
        EnemyAttackData jumpAttack = new EnemyAttackData()
        {
            atk = atkTrm.GetComponent<JumpAttack>(),
            AttackName = SkillName.Jump,
            action = () => {
                Debug.Log("Call");
                _stateInfo.IsJump = false;
                _stateInfo.IsAttack = false;
            },
            coolTime = 6f,
            damage = 1
        };
        EnemyAttackData tongueAttack = new EnemyAttackData()
        {
            atk = atkTrm.GetComponent<FrogTongueAttack>(),
            AttackName = SkillName.Range,
            action = () => {
                _stateInfo.IsRange = false;
                _stateInfo.IsAttack = false;
            },
            coolTime = 5f,
            damage = 1
        };

        _attackDictionary.Add(jumpAttack.AttackName, jumpAttack);
        _attackDictionary.Add(tongueAttack.AttackName, tongueAttack);

        foreach(var skill in _attackDictionary.Values)
        {
            _attackCoolList.Add(skill.AttackName, skill.coolTime);
        }
    }

    public virtual void Attack(SkillName skillname)
    {
        if (_stateInfo.IsAttack) return;
        if (isCoolDown(skillname) == false) return;

        FieldInfo fInfoBool = typeof(AIStateInfo)
        .GetField($"Is{skillname.ToString()}", BindingFlags.Public | BindingFlags.Instance);
            
        EnemyAttackData atkData = null;
        if(_attackDictionary.TryGetValue(skillname, out atkData)){
            _movement.StopImmediatelly();
            _stateInfo.IsAttack = true;
            fInfoBool.SetValue(_stateInfo, true); //??? ??? ?????????? ????
            SetCoolDown(atkData.AttackName, atkData.coolTime);
            atkData.atk.Attack(atkData.action);
            
        }
    }
    public bool isCoolDown(SkillName key)
    {
        float coolDown;
        if (_attackCoolList.TryGetValue(key, out coolDown))
        {
            //Debug.Log(
            //    $"{key.ToString()} : {Time.time > coolDown}");
            return Time.time > coolDown;
        }
        else
        {
            return false;
        }
    }

    public void SetCoolDown(SkillName key, float duration)
    {
        float coolDown = Time.time + duration;
        if (_attackCoolList.ContainsKey(key))
        {
            _attackCoolList[key] = coolDown;
        }
        else
        {
            _attackCoolList.Add(key, coolDown);
        }
    }
}
