using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Stage : MonoBehaviour
{
    public Transform PlayerStartPoint;
    public Transform PortalPoint;

    private void Start()
    {
        GameManager.instance.Target.position = PlayerStartPoint.position;
        StageManager.Instance.ProtalObj.transform.position = PortalPoint.position;

        DefineETC.VCam.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = transform.Find("CamConfiner").GetComponent<Collider2D>();
    }
}
