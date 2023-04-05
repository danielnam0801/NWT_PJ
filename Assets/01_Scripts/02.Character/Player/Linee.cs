using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Linee : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject sword;
    public GameObject player;

    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points = new List<Vector2>();

    private bool isDraw = false; //���� �����̴���
    private bool isCanSwordmove = false; //���콺�� �׸�����

    private void Update()
    {
        if (!isDraw)
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
        //�׸��� ���� �׸��� ����
        if (Input.GetMouseButtonDown(0))
        {
            points.Clear();
            GameObject go = Instantiate(linePrefab);
            lr = go.GetComponent<LineRenderer>();
            col = go.GetComponent<EdgeCollider2D>();
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
                col.points = points.ToArray(); // ����Ʈ�� �迭�ιٲ�
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraw = true;
            isCanSwordmove = false;

            if (points.Count > 5)
            {
                isCanSwordmove = true;
                StartCoroutine(SwordMove());
                isDraw = true;
            }
            isDraw = false;
        }
    }

    private IEnumerator SwordMove()
    {
        isDraw = false;
        //isCanSwordmove = false;
        //draw and delay
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 pos = new Vector3(points[i].x, points[i].y, 5);
            sword.transform.DOMove(pos, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log(points.Count);
        points.Clear();
        yield return new WaitForSeconds(0.2f);
    }
}
