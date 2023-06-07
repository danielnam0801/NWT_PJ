using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public abstract class GuideLine : PoolableObject
{
    //public ShapeType type;

    [SerializeField]
    protected float checkOffset = 0.3f;
    [SerializeField]
    protected float shapeSize = 1f;
    [SerializeField]
    protected Transform pair;


    protected float pathPointInterval;

    protected LineRenderer _lineRenderer;

    [SerializeField]
    protected List<Vector2> shapePoints;

    protected virtual void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    protected virtual void Start()
    {
        SetShapePoints();
        DrawShape();
    }

    public override void Init()
    {
        pathPointInterval = DrawManager.Instance.PathPointInterval;
        DrawManager.Instance.CheckLine += CheckShape;
    }

    private void Update()
    {
        transform.position = pair.position;
    }

    private void OnDisable()
    {
        if(DrawManager.Instance != null)
            DrawManager.Instance.CheckLine -= CheckShape;
    }

    protected abstract void SetShapePoints();

    private void DrawShape()
    {
        for(int i = 0; i < shapePoints.Count; i++)
        {
            _lineRenderer.positionCount = shapePoints.Count;
            _lineRenderer.SetPosition(i, shapePoints[i]);
        }
    }

    public void CheckShape(List<Vector2> points)
    {
        Debug.Log("check");

        if ((float)points.Count / (float)shapePoints.Count < 0.7f)
        {
            Debug.Log("return");
            return;
        }
            

        int count = 0;
        int repeat = points.Count < shapePoints.Count ? points.Count : shapePoints.Count;

        for (int i = 0; i < repeat; i++)
        {
            if(Vector2.Distance(shapePoints[i] + (Vector2)transform.position, points[i]) <= checkOffset)
            {
                count++;   
            }
        }

        if((float)count / (float)repeat >= 0.7f)
        {
            Debug.Log((float)count / (float)repeat);
            Debug.Log("그리기 성공");
        }
    }

    public void SetPair(Transform _pair)
    {
        pair = _pair;
    }
}
