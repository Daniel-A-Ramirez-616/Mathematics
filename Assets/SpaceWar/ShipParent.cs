using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipParent : MovingObject
{
    public DrawableObject ship;
    public DrawableObject thrust;
    Line LaserObject;
    public float ShipMaxVelocity = 25f;
    public float ShipThrust = 10f;
    public bool isDrawingLaser = false;
    public bool IsShipA = false;
    public float missleLaunchAt = 13f;
    public float LaserStart = 5f;
    public float LaserEnd = 100f;
    public float LaserShowTime = .5f;
    public float LaserShowCounter = 0f;

    public void SetupA(DrawableGrid grid, int sceneIndex)
    {
        IsShipA = true;
        ship = new ShipA();
        grid.AddObjectToScene(sceneIndex, ship);

        thrust = new ShipAThrust();
        grid.AddObjectToScene(sceneIndex, thrust);

        MaxVelocity = ShipMaxVelocity; 

        LaserObject = new Line();
        LaserObject.color = Color.yellow;
    }

    public void SetupB(DrawableGrid grid, int sceneIndex)
    {
        IsShipA = false ;
        ship = new ShipB();
        grid.AddObjectToScene(sceneIndex, ship);

        thrust = new ShipBThrust();
        grid.AddObjectToScene(sceneIndex, thrust);

        MaxVelocity = ShipMaxVelocity;

        LaserObject = new Line();
        LaserObject.color = Color.yellow;
    }

    public override void Tick()
    {
        base.Tick();
        UpdateSubObjects();
        UpdateLaser();
    }

    public void UpdateSubObjects()
    {
        ship.Position = this.Position;
        thrust.Position = this.Position;

        ship.Rotation = this.Rotation;
        thrust.Rotation = this.Rotation;

        ship.Scale = this.Scale;
        thrust.Scale = this.Scale;
    }

    public void UpdateLaser()
    {
        if (!isDrawingLaser){ return;}

        LaserShowCounter -= Time.deltaTime;

        if (LaserShowCounter < 0)
        {
            isDrawingLaser = false;
            return;
        }
        LaserObject.start = this.Position + DrawingTools.CircleRadiusPoint(Vector3.zero, GetRotationinDegrees(), LaserStart);
        LaserObject.end = this.Position + DrawingTools.CircleRadiusPoint(Vector3.zero, GetRotationinDegrees(), LaserEnd);

        SpaceWarGrid.self.DrawLine(LaserObject);
        LaserCollision();

    }

    public void LaserCollision()
    {
        foreach (MovingObject mo in SpaceWarGrid.self.MovingObjectlist)
        {
            if (mo == this)
            {
                //We found ourself
                
            }

            if (CollisionTools.DoesLineIntersectCircle(LaserObject.start, LaserObject.end, mo.CollisionCircle.Position,mo.CollisionRadius))
            {
                if (mo is ShipParent)
                {
                    SpaceWarGrid.self.RecordKill(IsShipA);
                }
                if (mo is Missle)
                {
                    Missle missle = (Missle)mo;
                    missle.RemoveMissle();
                }
            }
        }
    }

    public void AddThrust()
    {
        thrust.PerformDraw = true;
        Velocity += DrawingTools.CircleRadiusPoint(Vector3.zero, this.GetRotationinDegrees(), 1) * ShipThrust * Time.deltaTime;

    }

    public void NoThrust()
    {
        thrust.PerformDraw = false; 
    }

    public void RotateShip(float value)
    {
        float currentDegrees = GetRotationinDegrees();
        currentDegrees += value * Time.deltaTime;
        SetRotationinDegrees(currentDegrees);
    }

    public void FireMissle(DrawableGrid grid, int sceneIndex)
    {

        Missle missle = new Missle();
        
        missle.Position = CircleRadiusPoint(Position, GetRotationinDegrees(), 15);
        missle.CreateCollision(2, grid, sceneIndex);
        missle.willDrawCollision = true;
        missle.LaunchMissle(GetRotationinDegrees());
        SpaceWarGrid.self.AddObjectToScene(sceneIndex, missle);
        SpaceWarGrid.self.MovingObjectlist.Add(missle);

        //attempted to use factory method getting errors
        //missle.MakeMissle(Roation, Position, grid, sceneIndex);
    }

    public void FireLaser(DrawableGrid grid, int sceneIndex)
    {
        isDrawingLaser = true;
        LaserShowCounter = LaserShowTime;
    }
    public Vector3 CircleRadiusPoint(Vector3 origin, float angle, float radius)
    {
        Vector3 result = Vector3.zero;
        result.x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        result.y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        result += origin;

        return result;
    }

}
