using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    PoolableObject spawn;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spawn = PoolManager.Instance.Pop("Earth Armadilo");
            spawn.transform.position = this.transform.position;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            PoolManager.Instance.Push(spawn);
        }
    }
}
