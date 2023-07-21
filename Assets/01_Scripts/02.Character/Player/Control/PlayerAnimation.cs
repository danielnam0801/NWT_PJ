using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private int moveHash = Animator.StringToHash("Move");
    private int jumpHash = Animator.StringToHash("jump");
    public SpriteRenderer mask;

    private void Awake()
    {
        _animator = GetComponent<Animator>();   
    }

    public void SetMove(float value)
    {
        _animator.SetFloat(moveHash, value);
    }

    public void PlayJumpAnimation(bool value)
    {
        _animator.SetBool(jumpHash, value);  
    }

    public void Fade(float time = 1)
    {
        StartCoroutine(Fade_Coroutine(time));
    }

    private IEnumerator Fade_Coroutine(float time)
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        float percent = 0;
        float current = 0;
        Color bodyColor = render.color;
        Color maskColor = mask.color;

        while(percent <= 1)
        {
            current += Time.unscaledDeltaTime;
            percent = current / time;

            bodyColor = new Color(bodyColor.r, bodyColor.g, bodyColor.b, 1 - percent);
            maskColor = new Color(maskColor.r, maskColor.g, maskColor.b, 1 - percent);

            render.color = bodyColor;
            mask.color = maskColor;

            yield return null;
        }

        TimeManager.Instance.SetTimeScaleToLerp(1f, 1f);
    }
}
