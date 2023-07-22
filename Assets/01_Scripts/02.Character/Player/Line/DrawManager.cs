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

    [field: SerializeField]
    public bool canDraw { get; set; }
    public bool isDrawArea = false;
    public bool isMaxLength = false;
    //public bool OnTheWall = false;
    public LayerMask WallLayer;
    private GameObject go;

    [field:SerializeField]
    public bool isUIMouse { get; set; }

    public PlayerWeapon sword;
    public GameObject player;

    private Camera mainCam;
    private bool startDraw;
    public bool IsDraw { get; set; }

    private InGameUIController ui;

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
        //Cursor.lockState = CursorLockMode.Locked;

        mainCam = Camera.main;
        ui = FindObjectOfType<InGameUIController>();
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //    StartDraw();

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

    public void ToggleStartDraw()
    {
        if (!canDraw)
            return;

        startDraw = !startDraw;
    }

    private void Draw()
    {
        if(IsDraw)
        {
            currentDrawTime += Time.unscaledDeltaTime;
            if (currentDrawTime >= MaxDrawTime)
            {
                IsDraw = false;
                isMaxLength = false;
                //canDraw = true;
                DrawEndEvent?.Invoke();
                Destroy(go.gameObject);
                currentDrawTime = 0;
                SetDelayDraw(sword.Info.attackDelayTime);
            }
        }

        if (isUIMouse)
            return;

        //±×¸®´Â µµÁß ±×¸®¸é ¸ØÃã
        if (canDraw && startDraw)
        {
            //Cursor.lockState = CursorLockMode.None;

            startDraw = false;
            DrawStartEvent?.Invoke();
            IsDraw = true;
            canDraw = false;
            points.Clear();
            go = Instantiate(linePrefab);
            lr = go.GetComponent<LineRenderer>();
            lr.positionCount = 0;
            //points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //lr.positionCount = 1;
            //lr.SetPosition(0, points[0]);
        }
        else if (IsDraw && Input.GetMouseButton(0))
        {
            #region º® ¸¸³ª¸é ±×¸®±â ¸ØÃã
            //RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, WallLayer);
            //if (hit.collider != null)
            //{
            //    Debug.Log("On The Wall");
            //    OnTheWall = true;
            //}
            #endregion
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(points.Count == 0)
            {
                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(0, pos);
            }
            else
            {
                if (Vector2.Distance(points[points.Count - 1], pos) > pathPointInterval)
                {
                    points.Add(pos);
                    lr.positionCount++;
                    lr.SetPosition(lr.positionCount - 1, pos);
                }
            }

            if (lr.positionCount > maxLienLength)
                isMaxLength = true;
        }
        if (IsDraw && Input.GetMouseButtonUp(0) || isMaxLength /*|| OnTheWall*/)
        {
            ui.SetCoolSlider(0);
            //Cursor.lockState = CursorLockMode.Locked;
            currentDrawTime = 0;
            GuideLine guide = null;
            isMaxLength = false;
            IsDraw = false;
            //OnTheWall = false;
            Debug.Log(points.Count);

            DrawEndEvent?.Invoke();

            for (int i = 0; i < GuideLines.Count; i++)
            {
                bool success = GuideLines[i].CheckShape(points, out guide);

                if(success)
                {
                    if(guide.type == ShapeType.Circle)
                        points.Add(guide.transform.position);

                    SwordAttack(guide);
                    return;
                }
            }

            SwordAttack(guide);
        }
    }

    private void SwordAttack(GuideLine guide)
    {
        isDrawArea = false;
        Destroy(go.gameObject);
        Debug.Log("sword attack");
        canDraw = true;

        if (points.Count > minDrawPoint)
        {
            canDraw = false;
            sword.Attack(points, guide);
        }
    }
    private IEnumerator DelayDraw(float time)
    {
        float percent = 0;
        float current = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            ui.SetCoolSlider(percent);

            yield return null;
        }

        canDraw = true;
    }
}
