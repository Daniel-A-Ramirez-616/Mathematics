using Unity.VisualScripting;
using UnityEngine;

using Rect = System.Drawing.Rectangle; 

public class lab05Grid : DrawableGrid
{

    Rect box;
    Rect boxOnGrid;

    Line circleRadiusLine;
    float circleRadius = 10;
    Line EllipseRadiusLine;
    Vector2 ellipseAxis = new Vector2(10, 20);

    float offset;

    public override void SetupScenes()
    {
        int sceneIndex;
        //DrawableObject newGraph;

        SceneIndex = AddScene("Drawing Tools Test");

        box = new Rect(100, 100, 100, 100);
        boxOnGrid = new Rect(2, 3, 10, 10);


        offset = 0;

        circleRadiusLine = new Line(Vector3.zero, Vector3.zero, Color.cyan);
        EllipseRadiusLine = new Line(Vector3.zero, Vector3.zero, Color.magenta);



    }

    public override void Tick()
    {
        offset += Time.deltaTime;

        DrawingTools.DrawRectangle(box, Color.red);
        DrawingTools.DrawRectangle(boxOnGrid, Color.green,this);

        circleRadiusLine.end = DrawingTools.CircleRadiusPoint(Vector3.zero, offset * 90, circleRadius);
        DrawLine(circleRadiusLine);

        EllipseRadiusLine.end = DrawingTools.EllipseRadiusPoint(Vector3.zero, offset * 45, ellipseAxis);
        DrawLine(EllipseRadiusLine);

    }
}
