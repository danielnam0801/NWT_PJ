using System.Collections;
using UnityEngine;

public class StateMoveAgent : MonoBehaviour
{
    public bool RepeatMove;
    [SerializeField] private float _speed;
    [SerializeField] private float _moveTime;
    [SerializeField] private Vector3 repeatDir;

    private Vector3 dir;
    
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
