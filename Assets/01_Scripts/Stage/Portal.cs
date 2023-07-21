using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IInteract
{
    public float ActiveDistance = 7f;
    public float activeTime = 2.5f;
    public Vector2 DisableTiling = new Vector2(10, 10);
    public Vector2 EnableTiling = new Vector2(1, 1);

    private Transform playerTrm;
    private bool isActive = false;
    private Material mat;

    private void Start()
    {
        playerTrm = GameManager.instance.Target;
        mat = GetComponent<ParticleSystem>().GetComponent<Renderer>().material;

        Active(false);
    }

    public void Interact(GameObject Sender)
    {
        FadeManager.Instance.FadeOneShot(() =>
        {
            StageManager.Instance.ChangeStage();
            Active(false);
        }, 2, 1);
    }


    private void Update()
    {
        if(!isActive && Vector2.Distance(transform.position, playerTrm.position) < ActiveDistance)
        {
            Active(true);
        }
    }

    private void Active(bool value)
    {
        isActive = value;

        if(value)
        {
            StartCoroutine(Appear());
        }
        else
        {
            mat.mainTextureScale = DisableTiling;
        }
    }

    private IEnumerator Appear()
    {
        float current = 0;
        float percent = 0;
        Vector2 tiling = Vector2.zero;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / activeTime;

            tiling = Vector2.Lerp(DisableTiling, EnableTiling, percent);
            mat.mainTextureScale = tiling;

            yield return null;
        }
    }
}
