using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Grid2D : MonoBehaviour
{

    public Vector3 screenSize;
    public Vector3 origin;
    Vector2 localOffset;
    public Vector2 ScrollWheel;
    public Vector3 currentMousePos = Vector3.zero;
    public Vector3 cmpScreenSpace = Vector3.zero;
    public Vector3 cmpGridSpace = Vector3.zero;
    public Vector3 originOffset = Vector2.zero;

    public List<DrawingObject> drawObjects;


    public int index = 0;

    public float gridSize = 10f;
    float minGridSize = 2f;
    public float originSize = .6f;
    public float AxisOffset = 0f;



    int divisionCount = 5;
    int minDivisionCount = 2;

    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingOrigin = true;
    public bool willDrawOrigin = false;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;
    public bool isStillDrawingGrid = true;



    private void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);
        isStillDrawingGrid = true;
        drawObjects.Add(arrow);
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
        Keyboard kb = Keyboard.current;

        if (mouse != null)
        {
            //gets mouse position 
            currentMousePos = Mouse.current.position.ReadValue();
            //gets scroll wheel value and adjust gridsize appropriatly
            ScrollWheel = mouse.scroll.ReadValue();

            if (ScrollWheel.y < 0) { gridSize--; }
            if (ScrollWheel.y > 0) { gridSize++; }

            //gets mouse left click
            if (mouse.middleButton.wasPressedThisFrame)
            {
                origin.x = currentMousePos.x; 
                origin.y = currentMousePos.y; 
            
            }
        }

        if (kb.ctrlKey.isPressed && ScrollWheel.y > 0)
        {
            divisionCount++;
        }
        if(kb.ctrlKey.isPressed && ScrollWheel.y < 0)
        {
            divisionCount--;
        }

        if(kb.digit1Key.wasPressedThisFrame)
        {
            Debug.Log("key pressed");
            if(willDrawOrigin == true)
            {
                willDrawOrigin = false;
            }
            else
            {
                willDrawOrigin = true;
            }

        }
        if (kb.digit2Key.wasPressedThisFrame)
        {
            Debug.Log("key pressed");
            if (isDrawingAxis == true)
            {
                isDrawingAxis = false;
            }
            else
            {
                isDrawingAxis = true;
            }
        }
        if (kb.digit3Key.wasPressedThisFrame)
        {
            Debug.Log("key pressed");
            if (isDrawingDivisions == true)
            {
                isDrawingDivisions = false;
            }
            else
            {
                isDrawingDivisions = true;
            }
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
  
        isDrawingOrigin = true;

        while (isStillDrawingGrid)
        {

            localOffset = new Vector3(gridSize, gridSize) * index;
            AxisOffset = gridSize * originSize;
 
            DrawColor = lineColor;

            if ((isDrawingDivisions) && (index % divisionCount) == 0 && index != 0)
            {
                DrawColor = divisionColor;
  
            }

            if (index == 0 && isDrawingAxis == true)
            {
                DrawColor = axisColor;

            }

            if (isDrawingOrigin == true && willDrawOrigin == true)
            {
                DrawOrigin();
            }

            DrawGridLines(localOffset, DrawColor);
            DrawGridLines(-localOffset, DrawColor);

            index++;
            if (index == screenSize.x || index == screenSize.y)
            {
                isStillDrawingGrid = false;
                
            }
        }

        
        cmpGridSpace = ScreenToGrid(currentMousePos);
        cmpScreenSpace = GridToScreen(cmpGridSpace); ;
        

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

        DrawLine(new Vector2 (origin.x - AxisOffset, origin.y), new Vector3(origin.x, origin.y + AxisOffset), Color.blue);
        DrawLine(new Vector2(origin.x, origin.y + AxisOffset), new Vector3(origin.x + AxisOffset, origin.y), Color.blue);
        DrawLine(new Vector2(origin.x + AxisOffset, origin.y), new Vector3(origin.x, origin.y - AxisOffset), Color.blue);
        DrawLine(new Vector2(origin.x, origin.y - AxisOffset), new Vector3(origin.x - AxisOffset, origin.y), Color.blue);
      
    }

    /// <summary>
    /// Takes the potential grid space and outputs it into screen space
    /// </summary>
    /// <param name="gridSpace"></param>
    /// <returns>Vector3 translated to Screen Space</returns>
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
     return gridSpace * gridSize + origin;

    }

    /// <summary>
    /// Takes in screen space and outputs it as grid space
    /// </summary>
    /// <param name="screenSpace"></param>
    /// <returns>Vector3 translated to Grid Space</returns>
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        return (screenSpace - origin) / gridSize;
      
    }

    /// <summary>
    /// Draws the given line object. If you are creating new line object, use the overload that takes parameters instead. 
    /// </summary>
    /// <param name="line"></param>
    public void DrawLine(Line line, bool drawOnGrid = true)
    {
        Glint.AddCommand(line);
    }

    /// <summary>
    /// Draws a line, This overload takes line parameters
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    public void DrawLine(Vector3 start, Vector3 end, Color color, bool drawOnGrid = true)
    {
        Glint.AddCommand(new Line(start, end, color));
    }

    public static float V3ToAngle(Vector3 startPoint, Vector3 endPoint)
    {
        Mathf.Atan2(startPoint.x, endPoint.y);
        
        return 0f;
    }

    public static float LineToAngle(Line line)
    {
        return 0f;
    }

    public static Vector3 RotatePoint(Vector3 Center, float angle, Vector3 pointIN)
    {
        return Vector3.zero;
    }

    //Draws the Object at origin Point 
    public void DrawObject(DrawingObject lineObj, bool DrawOnGrid = true)
    {
        
    }

}