using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGuide : GuideLine
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private float unitChord;
    [SerializeField]
    private float unitAngle;
    private int pointCount;

    public override void Init()
    {
        base.Init();

        radius = shapeSize / 2f;
        unitChord = DrawManager.Instance.PathPointInterval;
        unitAngle = Mathf.Asin(unitChord / (2 * radius)) * 2;
        pointCount = (int)(360 / unitAngle);
    }

    protected override void SetShapePoints()
    {
        for(int i = 0; i < pointCount; i++)
        {
            shapePoints.Add(Quaternion.Euler(0, 0, unitAngle * i) * (transform.up * radius));
        }

        if(unitAngle * pointCount < 360f)
        {
            shapePoints.Add(shapePoints[0]);
        }

        //float angle = 0f;
        //float currentChord = 0f;

        //for (int i = 0; i < 100; i++)
        //{
        //    angle = Mathf.Asin(currentChord / (radius * 2)) * 2 * Mathf.Rad2Deg;
        //    if (angle > 179f)
        //        angle = 179f;
        //    Debug.Log(angle);
        //    currentChord += unitChord;
        //}

        //while (angle != 180f)
        //{
        //    angle = Mathf.Asin(currentChord / (radius * 2)) * 2 * Mathf.Rad2Deg;
        //    if (angle > 180f)
        //        angle = 180f;
        //    Vector2 point = Quaternion.Euler(0, 0, angle) * transform.up * radius;

        //    shapePoints.Add(point);
        //    Debug.Log(angle);
        //    Debug.Log(point);

        //    currentChord += unitChord;
        //}
    }
}
