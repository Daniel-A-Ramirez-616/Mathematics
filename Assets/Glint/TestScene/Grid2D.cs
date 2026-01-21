using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grid2D : MonoBehaviour
{

    public Vector3 screenSize;
    public Vector3 origin;
    Vector2 localOffset;
    public Vector2 ScrollWheel;
    Vector3 currentMousePos = Vector3.zero;
    Vector3 originOffset = Vector2.zero;

    int index = 0;

    public float gridSize = 10f;
    float minGridSize = 2f;
    public float originSize = .6f;
    public float AxisOffset = 0f;



    int divisionCount = 5;
    int minDivisionCount = 2;

    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingOrigin = false;
    public bool willDrawOrigin = false;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;
    public bool isStillDrawingGrid = true;



    private void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);
        isStillDrawingGrid = true;
    }

    void Update()
    {
        GetInput();
        DrawGrid();
        
    }

    /// <summary>
    /// Grabs Input 
    /// </summary>
    void GetInput()
    {
        Mouse mouse = Mouse.current;
        if (mouse != null)
        {
            //gets mouse position 
            currentMousePos = Mouse.current.position.ReadValue();
            //gets scroll wheel value and adjust gridsize appropriatly
            ScrollWheel = mouse.scroll.ReadValue();

            if (ScrollWheel.y < 0) { gridSize--; }
            if (ScrollWheel.y > 0) { gridSize++; }

            //gets mouse left click
            if (mouse.leftButton.wasPressedThisFrame) { origin.x = currentMousePos.x; origin.y = currentMousePos.y; }
        }
    }

    /// <summary>
    /// Draws the grid
    /// </summary>
    void DrawGrid()
    {
        isStillDrawingGrid = true;
        index = 0;
        Color DrawColor = Color.red;
        willDrawOrigin = true;
        while (isStillDrawingGrid)
        {

            localOffset = new Vector3(gridSize, gridSize) * index;

            DrawColor = lineColor;

            if ((index == 0) && (isDrawingAxis == true))
            {
                DrawColor = axisColor;
                willDrawOrigin = true;
            }

            if ((isDrawingDivisions) && (index % divisionCount) == 0)
            {
                DrawColor = divisionColor;
                willDrawOrigin = false;
            }

            DrawGridLines(localOffset, DrawColor);
            DrawGridLines(-localOffset, DrawColor);

            if (isDrawingOrigin == true && willDrawOrigin == true)
            {
                DrawOrigin();
            }

            index++;
            if (index == screenSize.x || index == screenSize.y)
            {
                isStillDrawingGrid = false;
                
            }
        }
 
    }

    public void DrawGridLines(Vector3 point, Color DrawColor)
    {
        DrawLine(new Vector2(origin.x + point.x, screenSize.y), new Vector2(origin.x + point.x, 0), DrawColor);
        DrawLine(new Vector2(0, origin.y + point.x), new Vector2(screenSize.x, origin.y + point.x), DrawColor);
    }

    /// <summary>
    /// Draws the Diamond symbol at the Origin
    /// </summary>
    public void DrawOrigin()
    {
        originOffset = origin * originSize;

        DrawLine(new Vector2 (originOffset.x, origin.y), new Vector3(originOffset.y, origin.x) ,Color.blue);
    }

    /// <summary>
    /// Takes the potential grid space and outputs it into screen space
    /// </summary>
    /// <param name="gridSpace"></param>
    /// <returns>Vector3 translated to Screen Space</returns>
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Takes in screen space and outputs it as grid space
    /// </summary>
    /// <param name="screenSpace"></param>
    /// <returns>Vector3 translated to Grid Space</returns>
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Draws the given line object. If you are creating new line object, use the overload that takes parameters instead. 
    /// </summary>
    /// <param name="line"></param>
    public void DrawLine(Line line)
    {
        Glint.AddCommand(line);
    }

    /// <summary>
    /// Draws a line, This overload takes line parameters
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    public void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        Glint.AddCommand(new Line(start, end, color));
    }

    //Draws the Origin Point (or Symbol)

}