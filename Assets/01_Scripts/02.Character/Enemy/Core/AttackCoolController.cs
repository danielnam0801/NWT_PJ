using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum EnemyType
{
    BigFrogEnemy,
    SmallFrog,
    Onion,
    Gun,
    Caterpillar,
    Bug,
    BeeHive,
    BubbleBee,
    Armadilo,
}

public class AttackCoolController : MonoBehaviour
{
    public EnemyType enemyType;
    Dictionary<SkillName, float> _attackCoolList;
    Dictionary<SkillName, EnemyAttackData> _attackDictionary;
    Enemy _enemy;
    EnemyMovement _movement;
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
        switch (enemyType)
        {
            case EnemyType.BigFrogEnemy:
                #region BigFrogEnemy
                EnemyAttackData jumpAttack = new EnemyAttackData()
                {
                    atk = atkTrm.GetComponent<JumpAttack>(),
                    AttackName = SkillName.Jump,
                    action = () => {
                        _stateInfo.IsJump = false;
                        _stateInfo.IsAttack = false;
                    },
                    coolTime = 5f,
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
                    coolTime = 3f,
                    damage = 1
                };

                _attackDictionary.Add(jumpAttack.AttackName, jumpAttack);
                _attackDictionary.Add(tongueAttack.AttackName, tongueAttack);
                #endregion
                break;
            case EnemyType.Armadilo:
                #region Armadildo
                EnemyAttackData rollAttack = new EnemyAttackData()
                {
                    atk = atkTrm.GetComponent<RollAttack>(),
                    AttackName = SkillName.Normal,
                    action = () => {
                        _stateInfo.IsNormal = false;
                        _stateInfo.IsAttack = false;
                    },
                    coolTime = 10f,
                    damage = 1
                };
                _attackDictionary.Add(rollAttack.AttackName, rollAttack);
                #endregion
                break;
        }

        foreach (var skill in _attackDictionary.Values)
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
