using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementData : MonoBehaviour
{
    public Vector2 direction;
    public Vector2 beforeDirection;

    public Vector2 pointOfInterest;

    public bool canMove = true;
    public float thinkTime;

    [SerializeField]
    private float speed;
    public float Speed 
    { 
        get { return speed; } 
        set 
        {
            speed = value;
            enemyMovement.SetSpeed(speed);
        }
    }

    private EnemyMovement enemyMovement;

    private void Awake()
    {
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
    }

}
