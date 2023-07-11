using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private int moveHash = Animator.StringToHash("Move");
    private int jumpHash = Animator.StringToHash("jump");

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
}
