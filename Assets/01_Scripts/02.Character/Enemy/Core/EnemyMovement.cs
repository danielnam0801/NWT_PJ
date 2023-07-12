using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _knockBackSpeed = 4f, _knockBackTime = 0.4f;
    [SerializeField] private float lerpTime = 0.5f;
    
    protected float _currentVelocity = 0;
    protected float _knockBackVelocity;

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
    protected AIBrain _brain;
    protected Rigidbody2D rb;

    protected bool _isknockBack = false;
    private float _knockBackStartTime;

    protected virtual void Awake()
    {
        _enemy = transform.GetComponent<Enemy>();
        _brain = transform.GetComponent<AIBrain>();
        rb = GetComponent<Rigidbody2D>();
        _data = transform.Find("AI").GetComponent<AIMovementData>();
    }

    public void KnockBack()
    {
        _brain.UseBrain = false;
        _brain.AIMovementData.canMove = false;
        _isknockBack = true;

        _knockBackStartTime = Time.time;
        _movementdirection = _brain.AIActionData.HitNormal * -1 * _knockBackSpeed;
        _movementdirection.y = 0;
    }

    protected bool CalculateKnockBack()
    {
        float spendTime = Time.time - _knockBackStartTime; //넉백되고 지금까지 흐른 시간
        float ratio = spendTime / _knockBackTime;
        _knockBackVelocity = Mathf.Lerp(2, 0, ratio) * Time.fixedDeltaTime;

        return ratio < 1;
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
    private void LateUpdate()
    {
        _enemy.EnemyAnimator.SetSpeed(_currentVelocity);
    }
}
