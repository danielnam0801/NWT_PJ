using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    Enemy _enemy;

    protected Rigidbody2D rb;
    public UnityEvent<float> onVelocityChange;

    protected float _currentVelocity = 0;
    [SerializeField] private float lerpTime = 0.5f;
    protected Vector2 _movementdirection;

    protected AIMovementData _data;

    protected virtual void Awake()
    {
        _enemy = transform.GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        _data = transform.Find("AI").GetComponent<AIMovementData>();
    }

    public void MoveAgent(Vector2 moveInput)
    {   
        _movementdirection = moveInput;
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

    private void Update()
    {
        _enemy.EnemyAnimator.SetSpeed(_currentVelocity);
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
