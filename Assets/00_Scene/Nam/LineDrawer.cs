using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    [Space(30f)] 
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;

    Line currentLine;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) BeginDraw();
        if (currentLine != null) Draw();
        if(Input.GetMouseButtonUp(0)) EndDraw();
    }

    public void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPositionMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }   
    
    public void Draw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        currentLine.AddPoint(mousePosition);
    }

    public void EndDraw()
    {
        if(currentLine != null)
        {
            if(currentLine.pointsCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
    }
}
