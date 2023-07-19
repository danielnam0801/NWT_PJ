using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public abstract class GuideLine : PoolableObject
{
    public ShapeType type;

    [SerializeField]
    protected float checkOffset = 0.3f;
    [SerializeField]
    protected float shapeSize = 1f;
    [SerializeField]
    public EnemyGuide pair;

    protected float pathPointInterval;

    protected LineRenderer _lineRenderer;

    [SerializeField]
    protected List<Vector2> shapePoints;
    protected GameObject startPoint;

    protected virtual void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        startPoint = transform.Find("DrawStartPoint").gameObject;
    }

    protected virtual void Start()
    {
        SetShapePoints();
        DrawShape();

        startPoint.transform.position = shapePoints[0];
    }

    public override void Init()
    {
        pathPointInterval = DrawManager.Instance.PathPointInterval;
        DrawManager.Instance.AddGuideLine(this);
    }

    private void Update()
    {
        transform.position = pair.transform.position;
    }

    private void OnDisable()
    {
        //push

        if(DrawManager.Instance != null)
            DrawManager.Instance.RemoveGudieLine(this);
        
        pair?.ClearPair();
        pair = null;
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

    public bool CheckShape(List<Vector2> points, out GuideLine line)
    {
        if ((float)points.Count / (float)shapePoints.Count < 0.7f)
        {
            line = this;
            return false;
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
            Debug.Log("�׸��� ����");

            line = this;
            
            return true;
        }
        else
        {
            Debug.Log("�׸��� ����");
            line = this;
            return false;
        }
    }

    public void SetPair(EnemyGuide _pair)
    {
        pair = _pair;
        pair.SetPair(this);
    }
}
