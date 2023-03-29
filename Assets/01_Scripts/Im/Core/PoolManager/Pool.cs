using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableObject
{
    private T prefab;
    private Transform parent;
    private Stack<T> pool;
    
    public Pool(T _prefab, Transform _parent, int cnt = 5)
    {
        prefab = _prefab;
        parent = _parent;

        for(int i = 0; i < cnt; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.name = obj.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
        }
    }

    public T Pop()
    {
        T obj = null;

        if(pool.Count <= 0)
        {
            obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true);
        }

        obj.Init();

        return obj;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }
}
