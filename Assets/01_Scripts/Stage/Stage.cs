using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Transform PlayerStartPoint;
    public Transform PortalPoint;

    private void Start()
    {
        GameManager.instance.Target.position = PlayerStartPoint.position;
        StageManager.Instance.ProtalObj.transform.position = PortalPoint.position;
    }
}
