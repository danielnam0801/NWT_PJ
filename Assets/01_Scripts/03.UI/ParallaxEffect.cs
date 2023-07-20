using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Vector2 _parallaxRatio;//�� ���� ���� ����� ������ �ϰ� �������鼭 � �������� ����
    private Transform _mainCamTrm;
    private Vector3 _lastCamPos;

    private void Start()
    {
        _mainCamTrm = DefineETC.VCam.transform;
        transform.position = _mainCamTrm.position + Vector3.forward;
        _lastCamPos = _mainCamTrm.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMove = _mainCamTrm.position - _lastCamPos;

        transform.position += new Vector3(deltaMove.x * _parallaxRatio.x, deltaMove.y * _parallaxRatio.y);

        _lastCamPos = _mainCamTrm.position;
    }
}
