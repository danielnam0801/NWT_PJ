using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpArrawGuide : GuideLine
{
    private float distance = 0;
    private float betweenPointCount = 0;
    private Vector2 dir = Vector2.zero;

    public override void Init()
    {
        base.Init();
    }

    protected override void SetShapePoints()
    {
        List<Vector2> triangleVertex = new List<Vector2>();
        List<Vector2> squareVertex = new List<Vector2>();

        //삼각형
        triangleVertex.Add(Vector2.up * shapeSize);
        triangleVertex.Add(Vector2.left * shapeSize);
        triangleVertex.Add(Vector2.right * shapeSize);

        float diagonalLenght = Mathf.Sqrt(Mathf.Pow(shapeSize, 2) / 2f);
        //사각형
        for (int i = 0; i < 4; i++)
        {
            squareVertex.Add(((Vector2)(Quaternion.Euler(0, 0, 45 + 90 * i) * Vector2.up) * diagonalLenght) + Vector2.up * -(shapeSize / 2));
        }

        DrawLine(triangleVertex[0], triangleVertex[1]);
        DrawLine(triangleVertex[1], squareVertex[0]);
        DrawLine(squareVertex[0], squareVertex[1]);
        DrawLine(squareVertex[1], squareVertex[2]);
        DrawLine(squareVertex[2], squareVertex[3]);
        DrawLine(squareVertex[3], triangleVertex[2]);
        DrawLine(triangleVertex[2], triangleVertex[0]);
    }

    private void DrawLine(Vector2 start, Vector2 end)
    {
        distance = Vector2.Distance(start, end);
        dir = (end - start).normalized;

        betweenPointCount = distance / pathPointInterval;

        for (int j = 0; j <= betweenPointCount; j++)
        {
            shapePoints.Add(start + ((dir * pathPointInterval) * j));
        }
    }
}
