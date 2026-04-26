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

    public static void SetColor(DrawableObject thing, Color color)
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

    public static bool DoesLineIntersectCircle(Vector3 LineStart, Vector3 LineEnd, Vector3 CircleCenter, float CircleRadius)
    {
        List<Vector3> Points = IntersectionPoint(LineStart, LineEnd, CircleCenter, CircleRadius);

        if (Points.Count == 0)
        {
            return false;
        }

        return true;
    }
    public static bool DoesLineIntersectCircle(Vector3 LineStart, Vector3 LineEnd, Vector3 CircleCenter, float CircleRadius, DrawableObject Intersect1, DrawableObject Intersect2)
    {
        List<Vector3> points = IntersectionPoint(LineStart, LineEnd, CircleCenter, CircleRadius);

        if (points.Count == 0)
        {
            Intersect1.PerformDraw = false;
            Intersect2.PerformDraw = false;
            return false;
        }

        if (points.Count == 1)
        {
            Intersect1.PerformDraw = true;
            Intersect2.PerformDraw = false;

            Intersect1.Position = points[0];
            return true;
        }

        Intersect1.PerformDraw = true;
        Intersect2.PerformDraw = true;

        Intersect1.Position = points[0];
        Intersect2.Position = points[1];

        return true;
    }
public static List<Vector3> IntersectionPoint(Vector3 p1, Vector3 p2, Vector3 center, float radius)
    {
        List<Vector3> result = new List<Vector3>();

        Vector3 dp = new Vector3();
        Vector3[] sect;
        float a, b, c;
        float bb4ac;
        float mu1;
        float mu2;
        // get the distance between X and Z on the segment
        dp.x = p2.x - p1.x;
        dp.y = p2.y - p1.y;
        // I don't get the math here
        a = dp.x * dp.x + dp.y * dp.y;
        b = 2 * (dp.x * (p1.x - center.x) + dp.y * (p1.y - center.y));
        c = center.x * center.x + center.y * center.y;
        c += p1.x * p1.x + p1.y * p1.y;
        c -= 2 * (center.x * p1.x + center.y * p1.y);
        c -= radius * radius;
        bb4ac = b * b - 4 * a * c;
        if (Mathf.Abs(a) < float.Epsilon || bb4ac < 0)
        {
            // line does not intersect
            return result;
        }
        mu1 = (-b + Mathf.Sqrt(bb4ac)) / (2 * a);
        mu2 = (-b - Mathf.Sqrt(bb4ac)) / (2 * a);
        sect = new Vector3[2];
        sect[0] = new Vector3(p1.x + mu1 * (p2.x - p1.x), p1.y + mu1 * (p2.y - p1.y), 0);
        sect[1] = new Vector3(p1.x + mu2 * (p2.x - p1.x), p1.y + mu2 * (p2.y - p1.y), 0);




        if (IsInLineSegment(sect[0], p1, p2))
        {
            result.Add(sect[0]);

        }
        if (IsInLineSegment(sect[1], p1, p2))
        {
            result.Add(sect[1]);

        }

        return result;
    }
    public static bool IsInLineSegment(Vector3 point, Vector3 start, Vector3 end)
    {
        return (
            (Mathf.Min(start.x, end.x) <= point.x) && (point.x <= Mathf.Max(start.x, end.x)) 
            && (Mathf.Min(start.y, end.y) <= point.y) && (point.y <= Mathf.Max(start.y, end.y))
        );
        
    }
}
