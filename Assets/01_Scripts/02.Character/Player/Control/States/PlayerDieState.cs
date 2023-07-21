using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDieState : PlayerState
{
    private Collider2D col;

    [SerializeField]
    private float disableTime = 1f;
    [SerializeField]
    private bool isDisable = false;
    [SerializeField]
    private float fadeDelaytime = 1f;
    public UnityEvent fadeOutEvent;

    public override void Init(Transform root)
    {
        base.Init(root);
        
        col = root.GetComponent<Collider2D>();
    }

    public override void EnterState()
    {
        if(!isDisable)
        {
            StartCoroutine(Disable());
        }    
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }

    private IEnumerator Disable()
    {
        isDisable = true;
        col.enabled = false;

        yield return new WaitForSeconds(fadeDelaytime);

        Debug.Log("fade");
        FadeManager.Instance.Fade(0, 1, () =>
        {
            Cursor.lockState = CursorLockMode.None;
            fadeOutEvent?.Invoke();
        }, 4f);
    }
}
