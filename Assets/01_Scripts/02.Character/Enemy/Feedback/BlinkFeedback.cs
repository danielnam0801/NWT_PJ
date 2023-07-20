using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFeedback : Feedback
{
    [SerializeField]
    private List<SpriteRenderer> _renderers;
    [SerializeField]
    private float _blinkTime = 0.2f;

    private MaterialPropertyBlock[] _matPropBlock;
    
    private readonly int _blinkHash = Shader.PropertyToID("_MakeHit");
    // readonly 런타임
    // const 컴파일 타임

    Enemy enemy;
    private int length;

    private void Awake()
    {
        if(transform.parent.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy = transform.parent.GetComponent<Enemy>();
            _renderers = new List<SpriteRenderer>();
            _renderers = enemy.ActiveVisual;
        }
        
        length = _renderers.Count;
        _matPropBlock = new MaterialPropertyBlock[length];

        for(int i = 0; i < length; i++)
        {
            _matPropBlock[i] = new MaterialPropertyBlock();
            _renderers[i].GetPropertyBlock(_matPropBlock[i]);
        }
    }

    private IEnumerator MaterialBlink()
    {
        for(int i = 0; i < length; i++)
        {
            _matPropBlock[i].SetFloat(_blinkHash, 0.7f);
            _renderers[i].SetPropertyBlock(_matPropBlock[i]);
        }

        yield return new WaitForSeconds(_blinkTime);

        for (int i = 0; i < length; i++)
        {
            _matPropBlock[i].SetFloat(_blinkHash, 0);
            _renderers[i].SetPropertyBlock(_matPropBlock[i]);
        }
    }

    public override void CreateFeedBack()
    {
        StartCoroutine(MaterialBlink());
    }

    public override void FinishFeedBack()
    {
        StopAllCoroutines(); //모든 코루틴 중지
        for (int i = 0; i < length; i++)
        {
            _matPropBlock[i].SetFloat(_blinkHash, 0);
            _renderers[i].SetPropertyBlock(_matPropBlock[i]);
        }

    }
}
