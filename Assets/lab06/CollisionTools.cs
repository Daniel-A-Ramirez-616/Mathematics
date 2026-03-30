using Palmmedia.ReportGenerator.Core;
using System.Collections.Generic;
using UnityEngine;

using Rect = System.Drawing.Rectangle;
public static class CollisionTools 
{
    public static void DrawTriangle(TriangleData data, Color color, DrawableGrid grid = null)
    {
        Line lineA = new Line(data.PointA, data.PointB, color);
        Line lineB = new Line(data.PointB, data.PointC, color);
        Line lineC = new Line(data.PointC, data.PointA, color);

        if (grid == null)
        {
            // Info is in Screen Space 
            Glint.AddCommand(lineA);
            Glint.AddCommand(lineB);
            Glint.AddCommand(lineC); 
        }
        else
        {
            grid.DrawLine(lineA);
            grid.DrawLine(lineB);
            grid.DrawLine(lineC);
        }
    }

    public static void SetColor(DrawableObject thing,  Color color)
    {
        for (int i = 0; i < thing.LineList.Count; i++)
        {
            Line item = thing.LineList[i];
            item.color = color;
            thing.LineList[i] = item;

            // C# is acting weird... 
            // won't let me use foreach
            // wont' let me do LineList[i].color = color; 
        }
    }

    public static bool IsPointInCircle(Vector3 Point, Vector3 Center, float Radius)
    {
        return (Point - Center).magnitude < Radius;
    }

    public static bool IsPointInRectangle(Vector3 Point, Rect Box)
    {
        return Point.x >= Box.X && Point.x <= Box.X + Box.Width && Point.y >= Box.Y && Point.y <= Box.Y + Box.Height;
    }
    public static bool IsPointInTriangle(Vector3 Point, TriangleData Triangle)
    {

        Vector3 v0 = Triangle.PointC - Triangle.PointA;
        Vector3 v1 = Triangle.PointB - Triangle.PointA;
        Vector3 v2 = Point - Triangle.PointA;

        float dot00 = Vector3.Dot(v0, v0);
        float dot01 = Vector3.Dot(v0, v1);
        float dot02 = Vector3.Dot(v0, v2);
        float dot11 = Vector3.Dot(v1, v1);
        float dot12 = Vector3.Dot(v1, v2);

        float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
        float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

        return (u >= 0) && (v >= 0) && (u + v < 1);
    }
}
