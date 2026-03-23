using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

using Rect = System.Drawing.Rectangle;
public class DrawingTools
{
    /// <summary>
    /// Draw a Rectangle on the Screen
    /// </summary>
    /// <param name="box">Rectangle to draw</param>
    /// <param name="color">Color to draw, use Color.#### </param>
    /// <param name="grid"> if grid = null, info in Rect is in screen coordinates, else info is in grid space</param>
    public static void DrawRectangle(Rect box, Color color, DrawableGrid grid = null)
    {
        Line top = new Line(new Vector2(box.X, box.Y+box.Height), new Vector2((box.X + box.Width), box.Y+box.Height), color);
        Line left = new Line(new Vector2(box.X, box.Y + box.Height), new Vector2(box.X, box.Y), color);
        Line right = new Line(new Vector2((box.X + box.Width), (box.Y + box.Height)), new Vector2((box.X + box.Width), box.Y), color);
        Line bottom = new Line(new Vector2(box.X, box.Y), new Vector2((box.X+box.Width), box.Y), color);

       if (grid == null)
        {
            Glint.AddCommand(top);
            Glint.AddCommand(left);
            Glint.AddCommand(right);
            Glint.AddCommand(bottom);

        }
        else
        {
            grid.DrawLine(top);
            grid.DrawLine(left);
            grid.DrawLine(right);
            grid.DrawLine(bottom);
        }
    }

    /// <summary>
    /// Find a point on a circle with given information
    /// </summary>
    /// <param name="origin">Center of the Cirle</param>
    /// <param name="angle">Angle in degrees, 0 degrees at (1,0,0)</param>
    /// <param name="radius">length of the radius</param>
    /// <returns>point in Vector3</returns>
    public static Vector3 CircleRadiusPoint(Vector3 origin, float angle, float radius)
    {
        Vector3 result = Vector3.zero;
        result.x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        result.y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        result += origin;

        return result;
    }

    /// <summary>
    /// Find a point on an ellipse  
    /// </summary>
    /// <param name="origin">Center of the Cirle</param>
    /// <param name="angle">Angle in degrees, 0 degrees at (1,0,0)</param>
    /// <param name="axis">length and axis of the elipse, this is half of the width or height</param>
    /// <returns>point in Vector3</returns>
    public static Vector3 EllipseRadiusPoint(Vector3 origin, float angle, Vector3 axis)
    {
        Vector3 result = Vector3.zero;
        result.x = Mathf.Cos(angle * Mathf.Deg2Rad) * axis.x;
        result.y = Mathf.Sin(angle * Mathf.Deg2Rad) * axis.y;

        result += origin;

        return result;
    }

    /// <summary>
    /// Draw a Circle in Screen Space 
    /// </summary>
    /// <param name="position">Position to draw in Screen Space</param>
    /// <param name="radius">Circle radius</param>
    /// <param name="sides">How many Sides of the Object. If Sides Less than 3, defaults to 12</param>
    /// <param name="color">Color to draw, use Color.####</param>
    public static void DrawCircle(Vector3 position, float radius, int sides, Color color)
    {
        int numberofSides = sides;
        if(numberofSides < 3) { numberofSides = 12; }

        float degreeStep = 360 / numberofSides;
        Vector3 start = Vector3.zero;
        Vector3 end = Vector3.zero;
        Line newline;

        for (int i = 0; i < numberofSides; i++)
        {
            start = CircleRadiusPoint(position, (degreeStep * i), radius);
            end = CircleRadiusPoint(position, (degreeStep * (i+1)), radius);
            newline = new Line(start, end, color);

            Glint.AddCommand(newline);


        }
    }

    /// <summary>
    /// Draw an Elipse in Screen Space 
    /// </summary>
    /// <param name="position">Position to draw in Screen Space</param>
    /// <param name="axis">Half Width\Height of the Ellipse</param>
    /// <param name="sides">How many Sides of the Object. If Sides Less than 3, defaults to 12</param>
    /// <param name="color">Color to draw, use Color.####</param>
    public static void DrawEllipse(Vector3 position, Vector2 axis, int sides, Color color)
    {
        int numberofSides = sides;
        if (numberofSides < 3) { numberofSides = 12; }

        float degreeStep = 360 / numberofSides;
        Vector3 start = Vector3.zero;
        Vector3 end = Vector3.zero;
        Line newline;

        for (int i = 0; i < numberofSides; i++)
        {
            start = EllipseRadiusPoint(position, (degreeStep * i), axis);
            end = EllipseRadiusPoint(position, (degreeStep * (i + 1)), axis);
            newline = new Line(start, end, color);

            Glint.AddCommand(newline);


        }
    }

    public static DrawableObject CreateCircleObject(Vector3 position, float radius, int sides, Color color)
    {
        DrawableObject newCircle = new DrawableObject();

        int numberofSides = sides;
        if (numberofSides < 3) { numberofSides = 12; }

        float degreeStep = 360 / numberofSides;
        Vector3 start = Vector3.zero;
        Vector3 end = Vector3.zero;
        Line newline;

        for (int i = 0; i < numberofSides; i++)
        {
            start = CircleRadiusPoint(position, (degreeStep * i), radius);
            end = CircleRadiusPoint(position, (degreeStep * (i + 1)), radius);
            newline = new Line(start, end, color);

            newCircle.AddLineToObject(newline);


        }
        
        

        return newCircle;
    }

    public static DrawableObject CreateCircleObjectonMouse(Vector3 position, float radius, int sides, Color color)
    {
        DrawableObject newCircle = new DrawableObject();

        int numberofSides = sides;
        if (numberofSides < 3) { numberofSides = 12; }

        float degreeStep = 360 / numberofSides;
        Vector3 start = Vector3.zero;
        Vector3 end = Vector3.zero;
        Line newline;

        for (int i = 0; i < numberofSides; i++)
        {
            start = CircleRadiusPoint(position, (degreeStep * i), radius);
            end = CircleRadiusPoint(position, (degreeStep * (i + 1)), radius);
            newline = new Line(start, end, color);

            newCircle.AddLineToObject(newline);


        }

        return newCircle;
    }

    public static DrawableObject CreateEllipseObject(Vector3 position, Vector2 axis, int sides, Color color)
    {
        DrawableObject newEllipse = new DrawableObject();

        int numberofSides = sides;
        if (numberofSides < 3) { numberofSides = 12; }

        float degreeStep = 360 / numberofSides;
        Vector3 start = Vector3.zero;
        Vector3 end = Vector3.zero;
        Line newline;

        for (int i = 0; i < numberofSides; i++)
        {
            start = EllipseRadiusPoint(position, (degreeStep * i), axis);
            end = EllipseRadiusPoint(position, (degreeStep * (i + 1)), axis);
            newline = new Line(start, end, color);

            newEllipse.AddLineToObject(newline);


        }

        return newEllipse;
    }

}
