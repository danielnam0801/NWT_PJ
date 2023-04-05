using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField] private List<PoolableObject> poolObjs = new List<PoolableObject>();
    private Dictionary<string, Pool<PoolableObject>> pools = new Dictionary<string, Pool<PoolableObject>>();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        CreatePool();
    }

    private void CreatePool()
    {
        for(int i = 0; i < poolObjs.Count; i++)
        {
            Pool<PoolableObject> pool = new Pool<PoolableObject>(poolObjs[i], transform);
            pools.Add(poolObjs[i].gameObject.name, pool);
        }
    }

    public PoolableObject Pop(string name)
    {
        if(pools.ContainsKey(name))
        {
            return pools[name].Pop();
        }

        Debug.Log("풀 없음");

        return null;
    }

    public void Push(PoolableObject obj)
    {
        if(pools.ContainsKey(obj.gameObject.name))
        {
            pools[obj.gameObject.name].Push(obj);
        }

        Debug.Log("풀 없음");
    }
}
