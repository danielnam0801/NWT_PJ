using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextViewer : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsLayer;
    [SerializeField] private Transform _targetPos;
    [SerializeField] private float _rayDis;
    [SerializeField] private float perfactDis;

    private TMP_Text text;
    private Color _color;

    private void Awake()
    {
        
        text = GetComponent<TMP_Text>();
        _color = text.color;

        

    }

    private void Start()
    { 
        _color.a = 0;
        text.color = _color;
        Debug.Log(text.color.a);
    }

    private void Update()
    {
        
        if(Vector2.Distance(transform.position, _targetPos.position) <= _rayDis) 
        {

            _color.a = (Vector2.Distance(transform.position, _targetPos.position) + perfactDis) / (_rayDis + perfactDis);
            text.color = _color;

        }
        _color.a = 0;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _rayDis);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, perfactDis);
    }
}
