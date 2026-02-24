using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drawing.Glint;
using System;
using UnityEngine.UIElements;

[Serializable]
public class DrawableObject
{

    public bool PerformDraw = true;

    // Transform information 
    public Vector3 Position = Vector3.zero;
    public float Roation = 0;
    public Vector3 Scale = Vector3.one;

    public List<Line> LineList;

    public DrawableObject()
    {
        LineList = new List<Line>();
        Initalize();
    }

    public virtual void Initalize()
    {

    }

    public void AddLineToObject(Line line)
    {
        LineList.Add(line);
    }

    public void AddLineToObject(Vector3 start, Vector3 end, Color color)
    {
        LineList.Add(new Line(start, end, color));
    }


    /// <summary>
    /// Draws the Drawing Object Instance
    /// </summary>
    /// <param name="grid">Optional, When a Grid2d is applied, object is drawn relative to the grid and location is in Grid space</param>
    public virtual void Draw(DrawableGrid grid)
    {
        if (!PerformDraw) { return; }
        if (LineList == null) { return; }
        if (LineList.Count == 0) { return; }

        for (int i = 0; i < LineList.Count; i++)
        {
            grid.DrawLine(TranslateLinePoints(LineList[i]));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="line"></param>
    /// <returns>given line translated by scale, rotation, postion</returns>
    public Line TranslateLinePoints(Line line)
    {
        Line translatedLine = line;
        Vector3 start = translatedLine.start;
        Vector3 end = translatedLine.end;

        // Scale
        translatedLine.start = new Vector3(start.x, start.y) * Scale.y;
        translatedLine.end = new Vector3(end.x, end.y) * Scale.y;

        // Rotate

        // Position
        translatedLine.start += Position;
        translatedLine.end += Position;

        return translatedLine;
    }
}