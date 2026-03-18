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
        // stub code, replace this
        return Vector3.zero;
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
        // stub code, replace this
        return Vector3.zero;
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

    }

    public static DrawableObject CreateCircleObject(Vector3 position, float radius, int sides, Color color)
    {
        DrawableObject newCircle = new DrawableObject();

        // The heavy lift of building an circle is in DrawCircle
        // Reformat the code to use AddLineToObject(Vector3 start, Vector3 end, Color color)

        return newCircle;
    }

    public static DrawableObject CreateEllipseObject(Vector3 position, float radius, int sides, Color color)
    {
        DrawableObject newEllipse = new DrawableObject();

        // The heavy lift of building an ellipse is in DrawEllipse
        // Reformat the code to use AddLineToObject(Vector3 start, Vector3 end, Color color)

        return newEllipse;
    }

}
