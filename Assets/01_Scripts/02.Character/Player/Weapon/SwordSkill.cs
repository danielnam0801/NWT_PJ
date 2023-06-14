using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : MonoBehaviour
{
    [SerializeField]
    private float circleSkillTime = 3f;
    [SerializeField]
    private float circleSkillMoveSpeed = 3f;
    [SerializeField]
    private float circleSkillTurnSpeed = 3f;

    private Vector2 targetPos;

    private PlayerWeapon sword;

    private void Awake()
    {
        sword = GetComponent<PlayerWeapon>();
    }

    public void DoSKill(ShapeType type, Vector2 _targetPos)
    {
        targetPos = _targetPos;

        switch (type)
        {
            case ShapeType.Circle:
                CircleSkill();
                break;
            case ShapeType.Triangle:
                TriangleSkill();
                break;
            case ShapeType.Square:
                SquareSkill();
                break;
            case ShapeType.Pentagon:
                PentagonSkill();
                break;
            case ShapeType.Default:
                DefaultSkill();
                break;
        }
    }

    private void DefaultSkill()
    {
        DrawManager.Instance.SwordMove();
        //StartCoroutine(DrawManager.Instance.SwordMove());
    }
    

    private void CircleSkill()
    {
        sword.IsFollow = false;

        Vector2 startPos = transform.position;
        float moveSpeed = Vector3.Distance(startPos, targetPos);
        float currentSkillTime = 0f;

        while (Vector2.Distance(transform.position, targetPos) >= 0.1f)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, Time.deltaTime * circleSkillMoveSpeed);
        }

        while(currentSkillTime < circleSkillTime)
        {
            Quaternion rotation = transform.rotation;
            transform.rotation = Quaternion.Lerp(rotation, Quaternion.Euler(0, 0, rotation.z), Time.deltaTime * circleSkillTurnSpeed);
            currentSkillTime += Time.deltaTime;
        }

        sword.IsFollow = true;
    }

    private void TriangleSkill()
    {

    }

    private void SquareSkill()
    {

    }

    private void PentagonSkill()
    {

    }
}
