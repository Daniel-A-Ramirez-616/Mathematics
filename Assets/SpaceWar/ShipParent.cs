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
    

    public void SetupA(DrawableGrid grid, int sceneIndex)
    {
        ship = new ShipA();
        grid.AddObjectToScene(sceneIndex, ship);

        thrust = new ShipAThrust();
        grid.AddObjectToScene(sceneIndex, thrust);

        MaxVelocity = ShipMaxVelocity; 
    }

    public void SetupB(DrawableGrid grid, int sceneIndex)
    {
        ship = new ShipB();
        grid.AddObjectToScene(sceneIndex, ship);

        thrust = new ShipBThrust();
        grid.AddObjectToScene(sceneIndex, thrust);

        MaxVelocity = ShipMaxVelocity;
    }

    public override void Tick()
    {
        base.Tick();
        UpdateSubObjects();
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
