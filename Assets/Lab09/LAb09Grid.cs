using UnityEngine;
using UnityEngine.InputSystem; 

using Rect = System.Drawing.Rectangle;

public class LAb09Grid : DrawableGrid
{

    TriangleData triangleData;
    Color triDrawColor = Color.red;

    Rect rectangleData;
    Color rectDrawColor = Color.red;

    DrawableObject circleObject;
    float circleRadius = 15;

    DrawableObject pointObject;

    Line lineTest;
    DrawableObject startPoint;
    DrawableObject endPoint;
    DrawableObject Intersect1;
    DrawableObject Intersect2;

    public string CircleCollisionResult = "NO";
    public string RectangleCollisionResult = "NO";
    public string TriangleCollisionResult = "NO";
    public string LineCircleIntersection = "NO";

    public override void SetupScenes()
    {
        // We'll have only one object in the scene. 
        int sceneIndex = AddScene("Lab 06: Collision Tools");

        circleObject = DrawingTools.CreateCircleObject(Vector3.zero, circleRadius, 36, Color.red);
        circleObject.Position = new Vector3(20, -25, 0);
        AddObjectToScene(sceneIndex, circleObject);

        pointObject = DrawingTools.CreateCircleObject(Vector3.zero, .5f, 12, Color.yellow);
        AddObjectToScene(sceneIndex, pointObject);

        rectangleData = new Rect(10, 15, 40, 30);

        triangleData = TriangleData.MakeTriangle(new Vector3(-40, -40, 0),
                                            new Vector3(-10, -30, 0),
                                            new Vector3(-30, -10, 0));

        lineTest = new Line(new Vector3(5, 5, 0), new Vector3(10, 5, 0), Color.cyan);
        
        startPoint = DrawingTools.CreateCircleObject(Vector3.zero, .25f, 12, Color.cyan);
        AddObjectToScene(sceneIndex, startPoint);

        endPoint = DrawingTools.CreateCircleObject(Vector3.zero, .25f, 12, Color.cyan);
        AddObjectToScene(sceneIndex, endPoint);

        Intersect1 = DrawingTools.CreateCircleObject(Vector3.zero, .5f, 12, Color.yellow);
        AddObjectToScene(sceneIndex, Intersect1);

        Intersect2 = DrawingTools.CreateCircleObject(Vector3.zero, .5f, 12, Color.yellow);
        AddObjectToScene(sceneIndex, Intersect2);

    }

    public override void ProcessInput(Keyboard kb, Mouse mouse)
    {

        if (mouse.leftButton.isPressed)
        {
            lineTest.start = ScreenToGrid(MousePosition);
        }

        if (mouse.rightButton.isPressed)
        {
            lineTest.end = ScreenToGrid(MousePosition);
        }


    }


    public override void Tick()
    {
        pointObject.Position = ScreenToGrid(MousePosition);




        if (CollisionTools.IsPointInCircle(pointObject.Position, circleObject.Position, circleRadius))
        {
            CollisionTools.SetColor(circleObject, Color.green);
            CircleCollisionResult = "YES";
        }
        else
        {
            CollisionTools.SetColor(circleObject, Color.red);
            CircleCollisionResult = "NO ";
        }

        if (CollisionTools.IsPointInRectangle(pointObject.Position, rectangleData))
        {
            rectDrawColor = Color.green;
            RectangleCollisionResult = "YES";
        }
        else
        {
            rectDrawColor = Color.red;
            RectangleCollisionResult = "NO ";
        }

        if (CollisionTools.IsPointInTriangle(pointObject.Position, triangleData))
        {
            triDrawColor = Color.green;
            TriangleCollisionResult = "YES";
        }
        else
        {
            triDrawColor = Color.red;
            TriangleCollisionResult = "NO ";
        }


        if (CollisionTools.DoesLineIntersectCircle(lineTest.start, lineTest.end, circleObject.Position, circleRadius, Intersect1, Intersect2))
        {
            LineCircleIntersection = "YES";
            lineTest.color = Color.magenta; 
        }
        else
        {
            LineCircleIntersection = "NO ";
            lineTest.color = Color.cyan;
        }



        /// Draw calls 
        DrawingTools.DrawRectangle(rectangleData, rectDrawColor, this);

        CollisionTools.DrawTriangle(triangleData, triDrawColor, this);

        startPoint.Position = lineTest.start;
        endPoint.Position = lineTest.end;

        DrawLine(lineTest);

    }
}