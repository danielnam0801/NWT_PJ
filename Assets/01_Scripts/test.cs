using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    GuideLine obj;
    public ShapeType shapeType;

    private void Start()
    {
        obj = PoolManager.Instance.Pop($"{shapeType}GuideLine") as GuideLine;
        //obj.SetPair(transform.Find("GuidePosition"));
    }
}
