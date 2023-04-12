using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public IEnumerator Fade(float fadeTime)
    {
        float currentFadeTime = 0;
        float alpha = 0;
        Gradient gradient = new Gradient();

        //라인 페이드
        while (currentFadeTime <= fadeTime)
        {
            currentFadeTime += Time.deltaTime;
            alpha = 1 - (currentFadeTime / fadeTime);

            GradientAlphaKey[] _alphaKeys = new GradientAlphaKey[2]
            {
                new GradientAlphaKey(alpha, 0),
                new GradientAlphaKey(alpha, .8f)
            };

            gradient.SetKeys(gradient.colorKeys, _alphaKeys);

            _lineRenderer.colorGradient = gradient;

            yield return null;
        }

        Destroy(this.gameObject);
    }
}
