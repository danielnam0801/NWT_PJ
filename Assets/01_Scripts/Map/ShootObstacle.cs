using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObstacle : DirectionType
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float _speed;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        
    }
}
