using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Stage : MonoBehaviour
{
    public Transform PlayerStartPoint;
    public Transform PortalPoint;
    public string BGMName;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(BGMName);
        AudioManager.Instance.SetBGMVolume(0, 1, 2);

        GameManager.instance.Target.position = PlayerStartPoint.position;
        StageManager.Instance.ProtalObj.transform.position = PortalPoint.position;

        DefineETC.VCam.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = transform.Find("CamConfiner").GetComponent<Collider2D>();
    }
}
