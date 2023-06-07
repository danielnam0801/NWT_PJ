using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGuide : GuideLine
{
    private float distance = 0;
    private float betweenPointCount = 0;
    private Vector2 startVertex = Vector2.zero;
    private Vector2 endVertex = Vector2.zero;
    private Vector2 dir = Vector2.zero;

    public override void Init()
    {
        base.Init();
    }

    protected override void SetShapePoints()
    {
        List<Vector2> vertex = new List<Vector2>();
        
        for(int i = 0; i < 4; i++)
        {
            vertex.Add((Quaternion.Euler(0, 0, 45 + 90 * i) * Vector2.up) * shapeSize);
        }

        for(int i = 0; i < vertex.Count; i++)
        {
            if(i == vertex.Count - 1)
            {
                startVertex = vertex[i];
                endVertex = vertex[0];
            }
            else
            {
                startVertex = vertex[i];
                endVertex = vertex[i + 1];
                
            }

            distance = Vector2.Distance(startVertex, endVertex);
            dir = (endVertex - startVertex).normalized;

            betweenPointCount = distance / pathPointInterval;

            for(int j = 0; j <= betweenPointCount; j++)
            {
                shapePoints.Add(startVertex + ((dir * pathPointInterval) * j));
            }
        }

        if (betweenPointCount > (int)betweenPointCount)
            shapePoints.Add(vertex[0]);
    }
}
