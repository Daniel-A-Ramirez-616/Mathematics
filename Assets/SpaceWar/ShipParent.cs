using Unity.VisualScripting;
using UnityEngine;

public class ShipParent : MovingObject
{
    public DrawableObject ship;
    public DrawableObject thrust;
    public float ShipMaxVelocity = 25;

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

        ship.Roation = this.Roation;
        thrust.Roation = this.Roation;

        ship.Scale = this.Scale;
        thrust.Scale = this.Scale;
    }
     

    public void AddThrust()
    {
        thrust.PerformDraw = true; 
    }

    public void NoThrust()
    {
        thrust.PerformDraw = false; 
    }

    public void RotateShip(float value)
    {

    }

    public void FireMissle(DrawableGrid grid, int sceneIndex)
    {

    }

    public void FireLaser(DrawableGrid grid, int sceneIndex)
    {

    }
}
