using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGuide : GuideLine
{
    public float radius;
    public float unitChord;

    public override void Init()
    {
        base.Init();

        radius = shapeSize / 2f;
        unitChord = DrawManager.Instance.PathPointInterval;
    }

    protected override void SetShapePoints()
    {
        for(int i = 0; i < shapeSize; i++)
        {
            shapePoints.Add(Quaternion.Equals(0, 0, i * 30) * (transform.up * radius));
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
