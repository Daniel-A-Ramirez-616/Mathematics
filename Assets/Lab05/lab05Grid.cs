using Unity.VisualScripting;
using UnityEngine;

using Rect = System.Drawing.Rectangle; 

public class lab05Grid : DrawableGrid
{

    Rect box;
    Rect boxOnGrid;

    Vector3 DrawTestPoint;

    Line circleRadiusLine;
    float circleRadius = 10;
    Line EllipseRadiusLine;
    Vector2 ellipseAxis;

    float offset;

    public override void SetupScenes()
    {
        int sceneIndex;
        DrawableObject newObject;

        sceneIndex = AddScene("Drawing Tools Test");

        newObject = DrawingTools.CreateCircleObject(Vector3.zero, 10, 36, Color.blue);
        AddObjectToScene(sceneIndex, newObject);

        newObject = DrawingTools.CreateEllipseObject(Vector3.zero, new Vector2(10, 20), 36, Color.blue);
        AddObjectToScene(sceneIndex, newObject);


        ellipseAxis = new Vector2(50, 75);
        box = new Rect(100, 100, 100, 100);
        boxOnGrid = new Rect(2, 3, 10, 10);


        offset = 0;

        circleRadiusLine = new Line(Vector3.zero, Vector3.zero, Color.cyan);
        EllipseRadiusLine = new Line(Vector3.zero, Vector3.zero, Color.magenta);

        DrawTestPoint = origin;
        DrawTestPoint.x *= .5f;

        circleRadiusLine.start = ScreenToGrid(DrawTestPoint);
        EllipseRadiusLine.start = ScreenToGrid(DrawTestPoint);
    }

    public override void Tick()
    {
        offset += Time.deltaTime;

        DrawingTools.DrawRectangle(box, Color.red);
        DrawingTools.DrawRectangle(boxOnGrid, Color.green,this);

        circleRadiusLine.end = DrawingTools.CircleRadiusPoint(ScreenToGrid(DrawTestPoint), offset * 90, circleRadius);
        DrawLine(circleRadiusLine);
        DrawingTools.DrawCircle(DrawTestPoint, circleRadius * gridSize, 36, Color.white);

        EllipseRadiusLine.end = DrawingTools.EllipseRadiusPoint(ScreenToGrid(DrawTestPoint), offset * 45, ellipseAxis);
        DrawLine(EllipseRadiusLine);
        DrawingTools.DrawEllipse(DrawTestPoint, ellipseAxis * gridSize, 12, Color.grey);
    }
}
