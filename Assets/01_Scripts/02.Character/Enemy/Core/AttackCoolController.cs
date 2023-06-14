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
    public SkillType MySkills; //꼭 할당된 공격과 맞는것으로 체크해줘야함
    public EnemyType enemyType;

    Dictionary<SkillType, float> _attackCoolList;
    Dictionary<SkillType, EnemyAttackData> _attackDictionary;
    Dictionary<SkillType, bool> _canAttackList;

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

    [Tooltip("공격사이에 공격간격두기")]
    [Header("AttackTerm")]
    public bool isUseAttackTerm = false;
    [SerializeField] private float attackTerm = 4f;

    Queue<EnemyAttackData> attackQueue;

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
        _attackCoolList = new Dictionary<SkillType, float>();
        _canAttackList = new Dictionary<SkillType, bool>();
        _attackDictionary = new Dictionary<SkillType, EnemyAttackData>();
        attackQueue = new Queue<EnemyAttackData>();
    }

    private void MakeAttackTypeAction()
    {
        Transform atkTrm = transform.Find("AttackType");

        if (MySkills.HasFlag(SkillType.Normal))
        { // NormalAttack이 있을때
            EnemyAttackData NormalAttack = new EnemyAttackData()
            {
                atk = atkTrm.GetComponent<INormalAttack>(),
                AttackName = SkillType.Normal,
                action = () =>
                {
                    isUseAttackTerm = true;
                    _stateInfo.IsNormal = false;
                    _stateInfo.IsAttack = false;
                    _canAttackList[SkillType.Normal] = true;
                    SetCoolDown(SkillType.Normal, normalCool);
                },
                coolTime = normalCool,
                damage = normalDamage
            };
            _attackDictionary.Add(NormalAttack.AttackName, NormalAttack);
            attackQueue.Enqueue(NormalAttack);
        }

        if (MySkills.HasFlag(SkillType.Special)) //SpecialAttack이 있을때
        {
            EnemyAttackData SpecialAttack = new EnemyAttackData()
            {
                atk = atkTrm.GetComponent<ISpecialAttack>(),
                AttackName = SkillType.Special,
                action = () =>
                {
                    _stateInfo.IsSpecial = false;
                    _stateInfo.IsAttack = false;
                    _canAttackList[SkillType.Special] = true;
                    SetCoolDown(SkillType.Special, specialCool);
                },
                coolTime = specialCool,
                damage = specialDamage
            };
            _attackDictionary.Add(SpecialAttack.AttackName, SpecialAttack);
            attackQueue.Enqueue(SpecialAttack);
        }

        if (MySkills.HasFlag(SkillType.Range)) //RagneAttack이 있을때
        {
            EnemyAttackData RangeAttack = new EnemyAttackData()
            {
                atk = atkTrm.GetComponent<IRangeAttack>(),
                AttackName = SkillType.Range,
                action = () =>
                {
                    _stateInfo.IsRange = false;
                    _stateInfo.IsAttack = false;
                    _canAttackList[SkillType.Range] = true;
                    SetCoolDown(SkillType.Range, rangeCool);
                },
                coolTime = rangeCool,
                damage = rangeDamage
            };
            _attackDictionary.Add(RangeAttack.AttackName, RangeAttack);
            attackQueue.Enqueue(RangeAttack);
        }

        if (MySkills.HasFlag(SkillType.Melee)) //MeleeAttack이 있을때
        {
            EnemyAttackData MeleeAttack = new EnemyAttackData()
            {
                atk = atkTrm.GetComponent<IMeleeAttack>(),
                AttackName = SkillType.Melee,
                action = () =>
                {
                    _stateInfo.IsMelee = false;
                    _stateInfo.IsAttack = false;
                    _canAttackList[SkillType.Melee] = true;
                    SetCoolDown(SkillType.Melee, meleeCool);
                },
                coolTime = meleeCool,
                damage = meleeDamage
            };
            _attackDictionary.Add(MeleeAttack.AttackName, MeleeAttack);
            attackQueue.Enqueue(MeleeAttack);
        }

        foreach (var skill in _attackDictionary.Values)
        {
            _attackCoolList.Add(skill.AttackName, skill.coolTime);
            _canAttackList.Add(skill.AttackName, true);
        }
    }

    public virtual bool Attack(SkillType skillname)
    {
        if (_stateInfo.IsAttack) return false;
        Debug.Log($"AttackQueue First : {attackQueue.Peek().AttackName}");
        
        if (attackQueue.Peek().AttackName != skillname) // 현재 들어온 공격이 우선순위 1순위가 아니라면
            return false;

        if (IsCanAttack(skillname) == false) return false;
        if (isCoolDown(skillname) == false) return false;


        EnemyAttackData atkData = null;
        if(_attackDictionary.TryGetValue(skillname, out atkData)){
            _movement.StopImmediatelly();
            SetAttackValue(skillname);
            _stateInfo.IsAttack = true;
            _canAttackList[skillname] = false;
            //SetCoolDown(atkData.AttackName, atkData.coolTime);
            atkData.atk.Attack(atkData.action);
            GotoEndQueue();
        }
        return true;
    }

    void GotoEndQueue()
    {
        EnemyAttackData temp = attackQueue.Peek(); //뒷순위로 미룬다
        attackQueue.Dequeue();
        attackQueue.Enqueue(temp); // 
    }

    void SetAttackValue(SkillType skill)
    {
        switch (skill)
        {
            case SkillType.Normal:
                _stateInfo.IsNormal = true;
                break;
            case SkillType.Special:
                _stateInfo.IsSpecial = true;
                break;
            case SkillType.Range:
                _stateInfo.IsRange = true;
                break;
            case SkillType.Melee:
                _stateInfo.IsMelee = true;
                break;
        }
    }

    public bool IsCanAttack(SkillType key)
    {
        bool value;
        if(_canAttackList.TryGetValue(key, out value))
        {
            return value;
        }
        else
        {
            return false;
        }
    }

    public bool isCoolDown(SkillType key)
    {
        float coolDown;
        if (_attackCoolList.TryGetValue(key, out coolDown))
        {
            return Time.time > coolDown;
        }
        else //들어온 key에 해당하는 value가 없음
        {
            return false;
        }
    }

    public void SetCoolDown(SkillType key, float duration)
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
