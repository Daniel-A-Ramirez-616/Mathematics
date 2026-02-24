using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class DrawableGrid : MonoBehaviour
{

    public Vector3 screenSize;
    public Vector3 origin;

    public float gridSize = 10f;
    public float minGridSize = 2f;
    public float originSize = .6f;

    int divisionCount = 5;
    int minDivisionCount = 2;

    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingObjects = true; 
    public bool isDrawingTheGrid = true; 
    public bool isDrawingOrigin = true;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;

    public int SceneIndex = 0; 
    public List<List<DrawableObject>> SceneList;
    public List<string> SceneListName;

    private void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);

        SceneList = new List< List<DrawableObject> >();
        SceneListName = new List<string>();
        SetupScenes(); 
    }

    public virtual void SetupScenes()
    {

    }

    void Update()
    {
        GetInput(); 
        DrawGrid();

        DrawScene(); 
    }

    public void SelectNextScene()
    {
        SceneIndex++; 
        if (SceneIndex >= SceneList.Count)
        {
            SceneIndex = 0; 
        }
    }

    public int AddScene(string newSceneName = null)
    {
        int sceneIndex = SceneList.Count; 
        SceneList.Add(new List<DrawableObject>());

        if (newSceneName != null)
        {
            SceneListName.Add(newSceneName);
        }
        else
        {
            SceneListName.Add("Scene #" + SceneListName.Count);
        }

        return sceneIndex; 
    }

    public void AddMultipleScenes(int newScenesToAdd)
    {
        if (newScenesToAdd <= 0)
        {
            Debug.LogWarning("0 or negative number scenes tried to be added");
            return;
        }

        for (int i = 0; i < newScenesToAdd; i++)
        {
            SceneList.Add(new List<DrawableObject>());
            SceneListName.Add("Scene #" + SceneListName.Count); 
        }
    }

    public void SetSceneName( int sceneNumber, string newSceneName)
    {
        if (sceneNumber >= SceneListName.Count)
        {
            Debug.LogWarning("Invalid Scene Number");
            return;
        }

        SceneListName[sceneNumber] = newSceneName; 
    }

    public string GetCurrentSceneName()
    {
        return SceneListName[SceneIndex];
    }

    public void AddObjectToScene(int sceneNumber, DrawableObject drawingObject)
    {
        if (sceneNumber >= SceneList.Count)
        {
            Debug.LogWarning("Invalid Scene Number");
            return;
        }

        SceneList[sceneNumber].Add(drawingObject);
    }

    public void DrawScene()
    {
        if (!isDrawingObjects) { return; }

        if (SceneList.Count == 0)
        {
            Debug.LogWarning("No Scenes to Draw!");
            return;
        }

        foreach (DrawableObject obj in SceneList[SceneIndex])
        {
            //Debug.Log("Drawing object");
            obj.Draw(this);
        }

    }



    /// <summary>
    /// Grabs Input 
    /// </summary>
    void GetInput()
    {
        Mouse mouse = Mouse.current;
        Keyboard kb = Keyboard.current;
        if ((kb == null) || (mouse == null))
        {
            Debug.LogError("Missing Keyboard or Mouse");
            return;
        }

        // Place the Origin 
        if (mouse.middleButton.isPressed)
        {
            origin = mouse.position.ReadValue();
        }

        // Check Mouse Scroll Wheel and update Grid Size
        bool ControlKey = kb.ctrlKey.isPressed;
        Vector2 scroll = mouse.scroll.ReadValue();

        // Adjust Grid Size 
        if ((scroll.y > 0) && !ControlKey)
        {
            gridSize++;
        }

        if ((scroll.y < 0) && !ControlKey)
        {
            gridSize--;
            if (gridSize <= minGridSize)
            {
                gridSize = minGridSize;
            }
        }

        // Adjust Divison Count 
        if ((scroll.y > 0) && ControlKey)
        {
            divisionCount++;
        }

        if ((scroll.y < 0) && ControlKey)
        {
            divisionCount--;
            if (divisionCount <= minDivisionCount)
            {
                divisionCount = minDivisionCount;
            }
        }

        if (kb.digit1Key.wasPressedThisFrame)
        {
            isDrawingDivisions = !isDrawingDivisions; 
        }

        if (kb.digit2Key.wasPressedThisFrame)
        {
            isDrawingAxis = !isDrawingAxis;
        }

        if (kb.digit3Key.wasPressedThisFrame)
        {
            isDrawingOrigin = !isDrawingOrigin;
        }

        if (kb.digit4Key.wasPressedThisFrame)
        {
            isDrawingTheGrid = !isDrawingTheGrid;
        }

        if (kb.digit5Key.wasPressedThisFrame)
        {
            isDrawingObjects = !isDrawingObjects;
        }

        if (kb.tabKey.wasPressedThisFrame)
        {
            SelectNextScene(); 
        }


    }

    /// <summary>
    /// Draws the grid
    /// </summary>
    void DrawGrid()
    {
        if (!isDrawingTheGrid)
        {
            // Ignore the rest of this function 
            return; 
        }

        Vector3 posPoint = Vector3.zero;
        Vector3 negPoint = Vector3.zero; 
        Vector3 pointOffset = Vector3.zero;
        int pointIndex = 0;

        Color drawColor = lineColor;

        bool StillDrawing = true;
        while (StillDrawing)
        {
            drawColor = lineColor;
            if ((pointIndex % divisionCount == 0) && isDrawingDivisions )
            {
                drawColor = divisionColor; 
            }
            if ( (pointIndex == 0 ) && isDrawingAxis )
            {
                drawColor = axisColor;
               
            }

            pointOffset = new Vector3(gridSize, gridSize, 0) * pointIndex; 
            posPoint = origin + pointOffset;
            negPoint = origin - pointOffset;

            DrawGridLines(posPoint, drawColor);
            DrawGridLines(negPoint, drawColor);

            pointIndex++;

            if ( IsOffScreen(negPoint) && IsOffScreen(posPoint))
            {
                StillDrawing = false;
            }
       
        }

        DrawOrigin();


    }

    public bool IsOffScreen(Vector3 point)
    {
        /// Can you tell me how to get to Seaseme Street 
        bool vertical    = ( (point.y < 0) || (point.y > screenSize.y) );
        bool horirzonal  = ( (point.x < 0) || (point.x > screenSize.x));

        return ( vertical && horirzonal );
    }

    public void DrawGridLines(Vector3 drawPoint, Color drawColor)
    {
        Vector3 Top     = new Vector3(drawPoint.x,  0,              0);
        Vector3 Bottom  = new Vector3(drawPoint.x,  screenSize.y,   0);
        Vector3 Left    = new Vector3(0,            drawPoint.y,    0);
        Vector3 Right   = new Vector3(screenSize.x, drawPoint.y,    0);

        DrawLine(Top, Bottom, drawColor, false); 
        DrawLine(Left, Right, drawColor, false);
    }

    /// <summary>
    /// Draws the Diamond symbol at the Origin
    /// </summary>
    public void DrawOrigin()
    {
        if (!isDrawingOrigin)
        {
            return; 
        }

        float offset = gridSize * originSize;
        //Debug.Log("draw origin: " + offset );

        Vector3 Top = origin;
        Top.y += offset; 
        Vector3 Bottom = origin;
        Bottom.y -= offset;
        Vector3 Left = origin;
        Left.x -= offset;
        Vector3 Right = origin; 
        Right.x += offset;

        DrawLine(Top, Left, axisColor, false);
        DrawLine(Left, Bottom, axisColor, false);
        DrawLine(Bottom, Right, axisColor, false);
        DrawLine(Right, Top, axisColor, false);

    }

    /// <summary>
    /// Takes the potential grid space and outputs it into screen space
    /// </summary>
    /// <param name="gridSpace"></param>
    /// <returns>Vector3 translated to Screen Space</returns>
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
        Vector3 result = origin + (gridSpace * gridSize); 

        return result;
    }

    /// <summary>
    /// Takes in screen space and outputs it as grid space
    /// </summary>
    /// <param name="screenSpace"></param>
    /// <returns>Vector3 translated to Grid Space</returns>
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        Vector3 result =  (screenSpace - origin) / gridSize;

        return result;
    }

    /// <summary>
    /// Draws the given line object. If you are creating new line object, use the overload that takes parameters instead. 
    /// </summary>
    /// <param name="line"></param>
    public void DrawLine(Line line, bool drawOnGrid = true)
    {
        if (!drawOnGrid)
        {
            Glint.AddCommand(line); 
            return;
        }
        DrawLine(line.start, line.end, line.color, true); 
    }

    /// <summary>
    /// Draws a line, This overload takes line parameters
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    public void DrawLine(Vector3 start, Vector3 end, Color color, bool drawOnGrid = true)
    {
        if (!drawOnGrid)
        {
            Glint.AddCommand(new Line(start, end, color));
            return; 
        }
        Glint.AddCommand(new Line(GridToScreen(start), GridToScreen(end), color));

    }
}