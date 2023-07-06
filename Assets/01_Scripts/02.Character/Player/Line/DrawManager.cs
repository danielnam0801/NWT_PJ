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
    //public Action<List<Vector2>> CheckLine;
    public List<GuideLine> GuideLines;

    public GameObject linePrefab;

    [SerializeField]
    private float pathPointInterval = 0.3f;
    [SerializeField]
    private float drawTimeScale = 0.2f;
    [SerializeField]
    private float maxLienLength = 35;
    [SerializeField]
    private int minDrawPoint = 5;
    [SerializeField]
    private float minAngleCheck = 45f;

    public float MaxDrawTime = 7.5f;
    [SerializeField]
    private float currentDrawTime = 0;

    public float PathPointInterval => pathPointInterval;

    LineRenderer lr;
    public List<Vector2> points = new List<Vector2>();

    private bool canDraw = true;
    public bool isDrawArea = false;
    public bool isMaxLength = false;
    private GameObject go;

    public PlayerWeapon sword;
    public GameObject player;

    private bool startDraw;
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
        startDraw = false;
        IsDraw = false;
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartDraw();

        Draw();
    }

    public void SetDelayDraw(float time)
    {
        StartCoroutine(DelayDraw(time));
    }

    public void AddGuideLine(GuideLine line)
    {
        GuideLines.Add(line);
    }

    public void RemoveGudieLine(GuideLine line)
    {
        if(GuideLines.Find(x => x == line) != null)
            GuideLines.Remove(line);
    }

    public void StartDraw()
    {
        startDraw = true;
    }

    private void Draw()
    {
        //if (!canDraw)
        //    return;

        //�׸��� ���� �׸��� ����
        if (canDraw && Input.GetMouseButtonDown(0))
        {
            //Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            startDraw = false;
            DrawStartEvent?.Invoke();
            IsDraw = true;
            canDraw = false;
            points.Clear();
            go = Instantiate(linePrefab);
            lr = go.GetComponent<LineRenderer>();
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lr.positionCount = 1;
            lr.SetPosition(0, points[0]);

        }
        else if (IsDraw && Input.GetMouseButton(0))
        {
            currentDrawTime += Time.unscaledDeltaTime;
            if (currentDrawTime >= MaxDrawTime)
            {
                IsDraw = false;
                isMaxLength = false;
                canDraw = true;
                DrawEndEvent?.Invoke();
                Destroy(go.gameObject);
                currentDrawTime = 0;
            }

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //pos.z = 5;
            if (Vector2.Distance(points[points.Count - 1], pos) > pathPointInterval)
            {
                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);
            }

            if (lr.positionCount > maxLienLength)
                isMaxLength = true;
        }
        if (IsDraw && Input.GetMouseButtonUp(0) || isMaxLength)
        {
            //Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            ShapeType _type = ShapeType.Default;
            isMaxLength = false;
            IsDraw = false;
            Debug.Log(points.Count);

            DrawEndEvent?.Invoke();

            for (int i = 0; i < GuideLines.Count; i++)
            {
                GuideLines[i].CheckShape(points, out _type, out Vector2 pos);

                if(_type != ShapeType.Default)
                {
                    if(_type == ShapeType.Circle)
                        points.Add(pos);

                    Debug.Log(points.Count);
                    SwordAttack(_type);
                    return;
                }

                //if(_type != ShapeType.Default && _type != ShapeType.Triangle)
                //    points.Add(GuideLines[i].transform.position);
            }

            Debug.Log(points.Count);
            SwordAttack(_type);
        }
    }

    private void SwordAttack(ShapeType _type)
    {
        isDrawArea = false;
        Destroy(go.gameObject);
        Debug.Log("sword attack");
        Debug.Log(_type);

        if (points.Count > minDrawPoint)
        {
            canDraw = false;
            sword.Attack(points, _type);
        }

        //points.Clear();
    }

    //private IEnumerator SwordMove()
    //{
    //    isDrawArea = false;
    //    Destroy(go.gameObject);
    //    if (points.Count > minDrawPoint)
    //    {
    //        canDraw = false;
    //        //yield return StartCoroutine(sword.Attack(points));
    //    }
        
    //    //CheckLine?.Invoke(points);

    //    points.Clear();
    //}

    private IEnumerator DelayDraw(float time)
    {
        yield return new WaitForSeconds(time);

        canDraw = true;
    }
}
