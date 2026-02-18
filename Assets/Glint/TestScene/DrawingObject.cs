using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drawing.Glint;
using System;

[Serializable]
public class DrawingObject
{
    public bool PerformDraw = true;

    public Vector3 Location = Vector3.zero; 
    public float Roation = 0;
    public Vector3 Scale = Vector3.zero;
    
    public List<Line> Lines;

    public DrawingObject()
    {
        Lines = new List<Line>();
        Initalize();
    }

    public virtual void Initalize()
    {

    }

    /// <summary>
    /// Draws the Drawing Object Instance
    /// </summary>
    /// <param name="grid">Optional, When a Grid2d is applied, object is drawn relative to the grid and location is in Grid space</param>
    public virtual void Draw(Grid2D grid)
    {

        if (!PerformDraw)
        {
           
            return;
        }

       
        if (Lines == null)
        {
           
            return; 
        }

        if (Lines.Count != 0)
        {
           
            for (int i = 0; i < Lines.Count; i++)
            {
              
                grid.DrawLine(Lines[i]);
            }
        
        }
        else
        {
            Debug.Log("No lines");
        }
            
    }
    
}
