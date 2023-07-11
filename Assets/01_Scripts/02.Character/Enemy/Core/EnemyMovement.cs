using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float lerpTime = 0.5f;
    protected float _currentVelocity = 0;

    protected Vector2 _movementdirection = Vector2.zero;
    public Vector2 MovementDirection
    {
        get { return _movementdirection; }
        set 
        {
            if (_movementdirection != Vector2.zero)
                _enemy.EnemyAnimator.Flip();
            _movementdirection = value; 
        }
    }

    protected AIMovementData _data;
    protected Enemy _enemy;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        _enemy = transform.GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        _data = transform.Find("AI").GetComponent<AIMovementData>();
    }

    private void Update()
    {
        _enemy.EnemyAnimator.SetSpeed(_currentVelocity);
    }

    public void MoveAgent(Vector2 moveInput)
    {
        MovementDirection = moveInput;
    }

    public void SetSpeed(float speed)
    {
        if (speed == 0)
        {
            StopImmediatelly();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(MoveLerp(speed));
        }
    }
    public void StopImmediatelly()
    {
        StopAllCoroutines();
        _currentVelocity = 0;
        rb.velocity = Vector2.zero;
    }

    IEnumerator MoveLerp(float speed)
    {
        float t = 0;
        while(t < lerpTime)
        {
            t += Time.deltaTime * lerpTime;
            yield return null;
            _currentVelocity = Mathf.Lerp(_currentVelocity, speed, t / lerpTime);
        }
    }


}
