using System; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Grid2D : MonoBehaviour
{

    public Vector3 screenSize;
    public Vector3 origin;
    public Vector2 ScrollWheel;
    public Vector3 currentMousePos = Vector3.zero;
    public Vector3 cmpScreenSpace = Vector3.zero;
    public Vector3 cmpGridSpace = Vector3.zero;
    Vector2 localOffset;
    Vector3 originOffset = Vector2.zero;

    public List<DrawingObject> drawingObjects;
    DrawingObject drawObj;

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
    public bool IsDrawingGrid = true;
    public bool isDrawingObjects = true; 



    private void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);
        isStillDrawingGrid = true;
       
        
        drawingObjects = new List<DrawingObject>();
        drawingObjects.Add(new Arrow());

        
    }

    void Update()
    {
        GetInput();
        DrawGrid();

        DrawObjects(); 


    }

    /// <summary>
    /// Grabs Input 
    /// </summary>
    void GetInput()
    {
        Mouse mouse = Mouse.current;
        Keyboard kb = Keyboard.current;

        if ((mouse == null) || (kb == null))
        {
            Debug.LogWarning("Missing Keyboard or Mouse");
            return;
        }


        //gets mouse position 
        currentMousePos = Mouse.current.position.ReadValue();
        //gets scroll wheel value and adjust gridsize appropriatly
        ScrollWheel = mouse.scroll.ReadValue();
        bool ControlIsPressed = kb.ctrlKey.isPressed; 

     

        //gets mouse left click
        if (mouse.middleButton.wasPressedThisFrame)
        {
            origin = currentMousePos;
        }

        if (!ControlIsPressed && (ScrollWheel.y > 0))
        {
            gridSize++;
        }
        if (!ControlIsPressed && (ScrollWheel.y < 0))
        {
            gridSize--;
            if (gridSize <= minGridSize)
            {
                gridSize = minGridSize;
            }
        }


        if (ControlIsPressed && (ScrollWheel.y > 0))
        {
            divisionCount++;
        }
        if (ControlIsPressed && (ScrollWheel.y < 0))
        {
            divisionCount--;
            if (divisionCount <= minDivisionCount)
            {
                divisionCount = minDivisionCount;
            }
        }

        if (kb.digit1Key.wasPressedThisFrame)
        {
            Debug.Log("key pressed");
            willDrawOrigin = !willDrawOrigin; 
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
        if (!IsDrawingGrid) {  return; }    


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
        DrawLine(new Vector2(origin.x + point.x, screenSize.y), new Vector2(origin.x + point.x, 0), DrawColor, false);
        DrawLine(new Vector2(0, origin.y + point.x), new Vector2(screenSize.x, origin.y + point.x), DrawColor, false);
    }

    /// <summary>
    /// Draws the Diamond symbol at the Origin
    /// </summary>
    public void DrawOrigin()
    {

        DrawLine(new Vector2 (origin.x - AxisOffset, origin.y), new Vector3(origin.x, origin.y + AxisOffset), Color.blue, false);
        DrawLine(new Vector2(origin.x, origin.y + AxisOffset), new Vector3(origin.x + AxisOffset, origin.y), Color.blue, false);
        DrawLine(new Vector2(origin.x + AxisOffset, origin.y), new Vector3(origin.x, origin.y - AxisOffset), Color.blue, false);
        DrawLine(new Vector2(origin.x, origin.y - AxisOffset), new Vector3(origin.x - AxisOffset, origin.y), Color.blue, false);
      
    }

    /// <summary>
    /// Takes the potential grid space and outputs it into screen space
    /// </summary>
    /// <param name="gridSpace"></param>
    /// <returns>Vector3 translated to Screen Space</returns>
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
     return (gridSpace * gridSize) + origin;

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
        if (drawOnGrid)
        {
            DrawLine(line.start, line.end, line.color, drawOnGrid); 
        }
        else
        {
            Glint.AddCommand(line);
        }
    }

    /// <summary>
    /// Draws a line, This overload takes line parameters
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    public void DrawLine(Vector3 start, Vector3 end, Color color, bool drawOnGrid = true)
    {
        if (drawOnGrid)
        {
            Glint.AddCommand(new Line(GridToScreen(start), GridToScreen(end), color));
        }
        else
        {
            Glint.AddCommand(new Line(start, end, color));
        }
    }

    public void DrawObjects()
    {
        if (!isDrawingObjects) { return; }  

        foreach (DrawingObject obj in drawingObjects)
        {
            obj.Draw(this); 
        }

        
    }

}