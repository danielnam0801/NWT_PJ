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
    public UnityEvent DrawStartEvent;
    public UnityEvent DrawEndEvent;

    public GameObject linePrefab;

    [SerializeField]
    private float pathPointInterval = 0.3f;
    [SerializeField]
    private float drawTimeScale = 0.2f;
    [SerializeField]
    private float maxLienLength = 35;
    [SerializeField]
    private int minDrawPoint = 5;

    LineRenderer lr;
    List<Vector2> points = new List<Vector2>();

    private bool canDraw = true;
    public bool isDrawArea = false;
    private bool isMaxLength = false;
    private GameObject go;

    public PlayerWeapon sword;
    public GameObject player;

    public bool StartDraw { get; set; }
    public bool IsDraw { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        StartDraw = false;
        IsDraw = false;
    }

    private void Update()
    {
        Draw();
    }

    public void SetDelayDraw(float time)
    {
        StartCoroutine(DelayDraw(time));
    }

    private void Draw()
    {
        if (!canDraw)
            return;

        //±×¸®´Â µµÁß ±×¸®¸é ¸ØÃã
        if (StartDraw)
        {
            StartDraw = false;
            DrawStartEvent?.Invoke();
            IsDraw = true;
            points.Clear();
            go = Instantiate(linePrefab);
            lr = go.GetComponent<LineRenderer>();
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lr.positionCount = 1;
            lr.SetPosition(0, points[0]);

        }
        else if (IsDraw && Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 5;
            if (Vector2.Distance(points[points.Count - 1], pos) > pathPointInterval)
            {
                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);
            }

            if (lr.positionCount > maxLienLength)
                isMaxLength = true;
        }
        else if (IsDraw && Input.GetMouseButtonUp(0) || isMaxLength)
        {
            isMaxLength = false;
            IsDraw = false;
            DrawEndEvent?.Invoke();

            StartCoroutine(SwordMove());
        }
    }

    private IEnumerator SwordMove()
    {
        isDrawArea = false;
        Destroy(go.gameObject);
        if (points.Count > minDrawPoint)
        {
            canDraw = false;
            yield return StartCoroutine(sword.Attack(points));
        }

        points.Clear();
    }

    private IEnumerator DelayDraw(float time)
    {
        yield return new WaitForSeconds(time);

        canDraw = true;
    }
}
