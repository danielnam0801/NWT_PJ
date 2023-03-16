using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    EdgeCollider2D _edgeCollider;
    LineRenderer _lineRenderer;
    Rigidbody2D _rb2d;

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;

    float pointsMinDistance = 0.1f;
    private void Awake()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void AddPoint(Vector2 point)
    {
        if (pointsCount >= 1 && Vector2.Distance(point, GetLastPoint()) < pointsMinDistance) return;
        points.Add(point);
        pointsCount++;
        _lineRenderer.positionCount = pointsCount;
        _lineRenderer.SetPosition(pointsCount - 1, point);

        if(pointsCount > 1)
        {
            _edgeCollider.points = points.ToArray();
        }

    }

    public Vector2 GetLastPoint() => (Vector2)_lineRenderer.GetPosition(pointsCount - 1);
    public void UsePhysics(bool isUse) => _rb2d.isKinematic = !isUse;
    public void SetLineColor(Gradient color) => _lineRenderer.colorGradient = color;
    public void SetPositionMinDistance(float distance) => pointsMinDistance = distance;
    public void SetLineWidth(float width)
    {
        _lineRenderer.startWidth = width;
        _lineRenderer.endWidth = width;
        _edgeCollider.edgeRadius = width / 2;
    }

}
