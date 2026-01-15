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

    int index = 0;
    
    float gridSize = 10f;
    float minGridSize = 2f;
    public float originSize = .6f;

    int divisionCount = 5;
    int minDivisionCount = 2;

    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingOrigin = false;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;
    public bool isStillDrawing = true;

    class rectangle
    {
        
    }

    private void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);
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

            if(ScrollWheel.y < 0) { gridSize--; }
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
        Color peterLine = Color.red; 
        while (isStillDrawing == true)
        {
            localOffset.x = index * gridSize;
            localOffset.y = index * gridSize;
            peterLine = lineColor;
            if((index == 0 ) && (isDrawingAxis == true))
            {
                peterLine = axisColor;
                
            }

            if(((index%divisionCount) == 0 ) && (isDrawingDivisions == true))
            {
                peterLine = divisionColor;
            }

            DrawLine(new Line(new Vector2(origin.x + localOffset.x, screenSize.y), new Vector2(origin.x + localOffset.x, 0), peterLine));
            DrawLine(new Line(new Vector2(0, origin.y + localOffset.x), new Vector2(screenSize.x, origin.y + localOffset.x), peterLine));

            DrawLine(new Line(new Vector2(origin.x - localOffset.x, screenSize.y), new Vector2(origin.x - localOffset.x, 0), peterLine));
            DrawLine(new Line(new Vector2(0, origin.y - localOffset.x), new Vector2(screenSize.x, origin.y - localOffset.x), peterLine));




            index++;
            if(index == 15)
            {
                isStillDrawing = false;
            }
        }

    }

    /// <summary>
    /// Draws the Diamond symbol at the Origin
    /// </summary>
    public void DrawOrigin()
    {
        
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