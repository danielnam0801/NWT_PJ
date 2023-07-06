using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public List<Transform> spwnPos = new List<Transform>();
    [SerializeField] private float _spwnCoolTime;
    private float _currentCoolTime;

    private void Awake()
    {
        _currentCoolTime = _spwnCoolTime;
    }

    private void Update()
    {
        _currentCoolTime -= Time.deltaTime;
        if(_currentCoolTime <= 0 )
        {
            int _random = Random.Range( 0, spwnPos.Count );
            var _icicle = PoolManager.Instance.Pop("Icicle");
            _icicle.transform.position = spwnPos[_random].position;
            _currentCoolTime = _spwnCoolTime;
        }
    }
}
