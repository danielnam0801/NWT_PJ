using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject sword;
    public GameObject player;

    [SerializeField]
    private float fadeTime = 1f;

    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points = new List<Vector2>();

    private bool isDraw = false; //검이 움직이는중
    private bool isSwordmove = false; //마우스로 그리는중
    private GameObject go;

    private void Update()
    {
        if (isSwordmove == false)
        {
            FollowPlayer();
        }

        Draw();
    }

    private void FollowPlayer()
    {
        sword.transform.DOMove(player.transform.position + new Vector3(-1, 1, 0), 1f);
    }

    private void Draw()
    {
        if (isSwordmove)
            return;

        //그리는 도중 그리면 멈춤
        if (Input.GetMouseButtonDown(0))
        {
            points.Clear();
            go = Instantiate(linePrefab);
            lr = go.GetComponent<LineRenderer>();
            col = go.GetComponent<EdgeCollider2D>();
            isDraw = true;
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lr.positionCount = 1;
            lr.SetPosition(0, points[0]);
            DOTween.KillAll(lr);
            
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 5;
            if (Vector2.Distance(points[points.Count - 1], pos) > 0.3f)
            {
                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);
                col.points = points.ToArray(); // 리스트를 배열로바꿈
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraw = false;

            if (points.Count > 5)
            {
                isSwordmove = true;
                DOTween.KillAll(sword);
                StartCoroutine(SwordMove());
            }
        }
    }

    private IEnumerator SwordMove()
    {
        //draw and delay
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 pos = new Vector3(points[i].x, points[i].y, 5);
            sword.transform.DOMove(pos, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }

        Debug.Log(points.Count);
        isSwordmove = false;
        points.Clear();

        StartCoroutine(go.GetComponent<Line>().Fade(fadeTime));
    }
}
