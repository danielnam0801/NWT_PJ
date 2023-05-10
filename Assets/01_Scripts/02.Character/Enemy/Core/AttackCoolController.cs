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
    public SkillName MySkills; //꼭 할당된 공격과 맞는것으로 체크해줘야함
    public EnemyType enemyType;
    Dictionary<SkillName, float> _attackCoolList;
    Dictionary<SkillName, EnemyAttackData> _attackDictionary;
    Enemy _enemy;
    EnemyMovement _movement;
    AIStateInfo _stateInfo;

    [Header("CoolValue")]
    [SerializeField] private float rangeCool = 5f;    
    [SerializeField] private float meleeCool = 3f;    
    [SerializeField] private float normalCool = 5f;    
    [SerializeField] private float specialCool = 10f;    

    [Header("DamageValue")]
    [SerializeField] private float rangeDamage = 5f;    
    [SerializeField] private float meleeDamage = 3f;    
    [SerializeField] private float normalDamage = 5f;    
    [SerializeField] private float specialDamage = 10f;

    public bool RangeBool;
    public bool MeleeBool;
    public bool SpecialBool;
    public bool NormalBool;

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

        if (MySkills.HasFlag(SkillName.Normal))
        { // NormalAttack이 있을때
            EnemyAttackData NormalAttack = new EnemyAttackData()
            {
                atk = atkTrm.GetComponent<INormalAttack>(),
                AttackName = SkillName.Normal,
                action = () =>
                {
                    _stateInfo.IsNormal = false;
                    _stateInfo.IsAttack = false;
                },
                coolTime = normalCool,
                damage = normalDamage
            };
            _attackDictionary.Add(NormalAttack.AttackName, NormalAttack);
        }

        if (MySkills.HasFlag(SkillName.Special)) //SpecialAttack이 있을때
        {
            EnemyAttackData SpecialAttack = new EnemyAttackData()
            {
                atk = atkTrm.GetComponent<ISpecialAttack>(),
                AttackName = SkillName.Special,
                action = () =>
                {
                    _stateInfo.IsSpecial = false;
                    _stateInfo.IsAttack = false;
                },
                coolTime = specialCool,
                damage = specialDamage
            };
            _attackDictionary.Add(SpecialAttack.AttackName, SpecialAttack);
        }

        if (MySkills.HasFlag(SkillName.Range)) //RagneAttack이 있을때
        {
            EnemyAttackData RangeAttack = new EnemyAttackData()
            {
                atk = atkTrm.GetComponent<IRangeAttack>(),
                AttackName = SkillName.Range,
                action = () =>
                {
                    _stateInfo.IsRange = false;
                    _stateInfo.IsAttack = false;
                },
                coolTime = rangeCool,
                damage = rangeDamage
            };
            _attackDictionary.Add(RangeAttack.AttackName, RangeAttack);
        }

        if (MySkills.HasFlag(SkillName.Melee)) //MeleeAttack이 있을때
        {
            EnemyAttackData MeleeAttack = new EnemyAttackData()
            {
                atk = atkTrm.GetComponent<IMeleeAttack>(),
                AttackName = SkillName.Melee,
                action = () =>
                {
                    _stateInfo.IsMelee = false;
                    _stateInfo.IsAttack = false;
                },
                coolTime = meleeCool,
                damage = meleeDamage
            };
            _attackDictionary.Add(MeleeAttack.AttackName, MeleeAttack);
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
            if(Time.time > coolDown)
            {
                if(key == SkillName.Normal)
                {
                    NormalBool = true;
                }
                if(key == SkillName.Special)
                {
                    SpecialBool = true;
                }
                if(key == SkillName.Range)
                {
                    RangeBool = true;
                }
                if(key == SkillName.Melee)
                {
                    MeleeBool = true;
                }
                return Time.time > coolDown;
            }
            return Time.time > coolDown;
        }
        else
        {
            if (key == SkillName.Normal)
            {
                NormalBool = false;
            }
            if (key == SkillName.Special)
            {
                SpecialBool = false;
            }
            if (key == SkillName.Range)
            {
                RangeBool = false;
            }
            if (key == SkillName.Melee)
            {
                MeleeBool = false;
            }
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
