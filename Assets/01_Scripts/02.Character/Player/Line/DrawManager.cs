using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DrawManager : MonoBehaviour
{
    public static DrawManager Instance;
    public UnityEvent DrawEndEvent;

    public GameObject linePrefab;

    [SerializeField]
    private float pathPointInterval = 0.3f;

    LineRenderer lr;
    List<Vector2> points = new List<Vector2>();

    private bool canDraw = true;
    private bool isDraw = false;
    private GameObject go;
    public PlayerWeapon sword;
    public GameObject player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (!canDraw)
            return;

        //±×¸®´Â µµÁß ±×¸®¸é ¸ØÃã
        if (Input.GetMouseButtonDown(0))
        {
            isDraw = true;
            points.Clear();
            go = Instantiate(linePrefab);
            lr = go.GetComponent<LineRenderer>();
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lr.positionCount = 1;
            lr.SetPosition(0, points[0]);

        }
        else if (isDraw && Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 5;
            if (Vector2.Distance(points[points.Count - 1], pos) > pathPointInterval)
            {
                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);
            }
        }
        else if (isDraw && Input.GetMouseButtonUp(0))
        {
            isDraw = false;

            StartCoroutine(SwordMove());
        }
    }

    private IEnumerator SwordMove()
    {
        canDraw = false;

        if (points.Count > 5)
        {
            yield return StartCoroutine(sword.Attack(points));
        }

        points.Clear();
        canDraw = true;

        yield return StartCoroutine(go.GetComponent<Line>().Fade());
    }
}
