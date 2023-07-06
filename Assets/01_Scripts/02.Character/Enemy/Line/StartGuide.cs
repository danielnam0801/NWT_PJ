using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGuide : GuideLine
{
    private float distance = 0;
    private float betweenPointCount = 0;
    public int PointCount;

    private List<Vector2> innerShapeVertex = new List<Vector2>();
    private List<Vector2> outerShapeVertex = new List<Vector2>();

    private Vector2 startVertex = Vector2.zero;
    private Vector2 endVertex = Vector2.zero;
    private Vector2 dir = Vector2.zero;

    public override void Init()
    {
        base.Init();

        innerShapeVertex.Clear();
        outerShapeVertex.Clear();
    }

    protected override void SetShapePoints()
    {
        Debug.Log("draw");

        //inner
        for (int i = 0; i < PointCount; i++)
        {
            innerShapeVertex.Add((Quaternion.Euler(0, 0, (90 - (PointCount + 1) * 9) + (360f / PointCount * i)) * Vector2.up) * (shapeSize / 2.5f));
        }

        //outter
        for(int i = 0; i < PointCount; i++)
        {
            outerShapeVertex.Add((Quaternion.Euler(0, 0, 360f / PointCount * i) * Vector2.up) * (shapeSize));
        }

        for (int i = 0; i < PointCount * 2; i++)
        {
            if(i != PointCount * 2 - 1)
            {
                if (i % 2 == 0)
                {
                    startVertex = outerShapeVertex[i / 2];
                    endVertex = innerShapeVertex[i / 2];
                }
                else
                {
                    startVertex = innerShapeVertex[i / 2];
                    endVertex = outerShapeVertex[i / 2 + 1];
                }
            }
            else
            {
                startVertex = innerShapeVertex[innerShapeVertex.Count - 1];
                endVertex = outerShapeVertex[0];
            }

            //if (i == vertex.Count - 1)
            //{
            //    startVertex = vertex[i];
            //    endVertex = vertex[0];
            //}
            //else
            //{
            //    startVertex = vertex[i];
            //    endVertex = vertex[i + 1];
            //}

            distance = Vector2.Distance(startVertex, endVertex);
            dir = (endVertex - startVertex).normalized;

            betweenPointCount = distance / pathPointInterval;

            for (int j = 0; j <= betweenPointCount; j++)
            {
                shapePoints.Add(startVertex + ((dir * pathPointInterval) * j));
            }
        }

        if (betweenPointCount > (int)betweenPointCount)
            shapePoints.Add(outerShapeVertex[0]);
    }
}
