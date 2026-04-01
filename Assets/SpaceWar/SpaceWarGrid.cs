using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.InputSystem; 

public class SpaceWarGrid : DrawableGrid
{
    public static SpaceWarGrid self; 


    public bool isPlayingGame = false;

    int sceneIndex = 0;

    public Missle missleObject;
    public ShipParent ShipAObject;
    public ShipParent ShipBObject;

    public DrawableObject DebugMagicCircle;
    public float MagicCircleRadius = 150; 

    public List<MovingObject> MovingObjectlist = new List<MovingObject>();


    // Game Score, This is Player Deaths
    public int PlayerAScore = 0;
    public int PlayerBScore = 0;


    // Player Inputs
    bool P1_Thrust = false; 
    bool P2_Thrust = false;
    bool P1_CWRotation = false;
    bool P2_CWRotation = false;
    bool P1_CCWRotation = false;
    bool P2_CCWRotation = false;
    bool P1_FireMissle = false;    
    bool P2_FireMissle = false;
    bool P1_FireLaser = false;
    bool P2_FireLaser = false;

    public void Awake()
    {
        self = this;
    }

    public override void SetupScenes()
    {
        sceneIndex = AddScene("Lab 07: SpaceWar");
        
        missleObject = new Missle();
        missleObject.Position = new Vector3(0, 15, 0);
        //missleObject.SetRotationinDegrees(75);
        missleObject.CreateCollision(2, this, sceneIndex);
        missleObject.willDrawCollision = true;
        missleObject.LaunchMissle(25);
        AddObjectToScene(sceneIndex, missleObject);
        MovingObjectlist.Add(missleObject);

        ShipAObject = new ShipParent();
        ShipAObject.SetupA(this, sceneIndex); 
        ShipAObject.Position = new Vector3(100, 0, 0);
        ShipAObject.SetRotationinDegrees(180);
        ShipAObject.CreateCollision(10, this, sceneIndex);
        ShipAObject.willDrawCollision = true;
        AddObjectToScene(sceneIndex, ShipAObject);
        MovingObjectlist.Add(ShipAObject);

        ShipBObject = new ShipParent();
        ShipBObject.SetupB(this, sceneIndex);
        ShipBObject.Position = new Vector3(-15, 0, 0);
        ShipBObject.CreateCollision(10, this, sceneIndex);
        ShipBObject.willDrawCollision = true; 
        AddObjectToScene(sceneIndex, ShipBObject);
        MovingObjectlist.Add(ShipBObject);

        DebugMagicCircle = DrawingTools.CreateCircleObject(Vector3.zero, MagicCircleRadius, 360, Color.gray);
        AddObjectToScene(sceneIndex, DebugMagicCircle);

    }

    public override void ProcessInput(Keyboard kb, Mouse mouse)
    {
        P1_Thrust = kb.wKey.isPressed;
        P1_CWRotation = kb.dKey.isPressed;
        P1_CCWRotation = kb.aKey.isPressed;
        P1_FireMissle = kb.qKey.wasPressedThisFrame;
        P1_FireLaser = kb.eKey.wasPressedThisFrame;

        P2_Thrust = kb.iKey.isPressed;
        P2_CWRotation = kb.lKey.isPressed;
        P2_CCWRotation = kb.jKey.isPressed;
        P2_FireMissle = kb.uKey.wasPressedThisFrame;
        P2_FireLaser = kb.oKey.wasPressedThisFrame;
    }



    public override void Tick()
    {
        // Remove Me!
        TestStuff();

        HandleInput(); 


    } 

    public void HandleInput()
    {
        if (P1_Thrust) { ShipAObject.AddThrust(); } else { ShipAObject.NoThrust(); }
        if (P1_CWRotation) { ShipAObject.RotateShip(45); }
        if (P1_CCWRotation) { ShipAObject.RotateShip(-45); }
        if (P1_FireMissle) { ShipAObject.FireMissle(this, sceneIndex); }
        if (P1_FireLaser) { ShipAObject.FireLaser(this, sceneIndex); ; }

        if (P2_Thrust) { ShipBObject.AddThrust(); } else { ShipBObject.NoThrust(); }
        if (P2_CWRotation) { ShipBObject.RotateShip(45); }
        if (P2_CCWRotation) { ShipBObject.RotateShip(-45); }
        if (P2_FireMissle) { ShipBObject.FireMissle(this, sceneIndex); }
        if (P2_FireLaser) { ShipBObject.FireLaser(this, sceneIndex); }
    }

    public void TestStuff()
    {
        ShipAObject.Roation += (30 * Time.deltaTime * Mathf.Deg2Rad);
        ShipBObject.Roation += (-30 * Time.deltaTime * Mathf.Deg2Rad);
    }

    public void StartGame()
    {
        isPlayingGame = true;
        isTickingScenes = true; 
    }

    public void GameOver()
    {
        isPlayingGame = false;
        isTickingScenes = false;
    }
}
