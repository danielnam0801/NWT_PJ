using System.Collections;
using UnityEngine;

public class StateMoveAgent : DirectionType
{
    public bool RepeatMove;
    [SerializeField] private float _speed;
    [SerializeField] private float _moveTime;

    private Vector3 dir;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(UpandDown());
    }

    private void Update()
    {
        transform.position += dir * _speed * Time.deltaTime;
    }
    private IEnumerator UpandDown()
    {
        if(RepeatMove)
        {
            while (true)
            {
                dir = repeatDir;
                yield return new WaitForSeconds(_moveTime);
                dir = -repeatDir;
                yield return new WaitForSeconds(_moveTime);
            }
        }
        else
        {
            dir = repeatDir;
            yield return new WaitForSeconds(_moveTime);
            dir = Vector3.zero;
        }
    }
}
