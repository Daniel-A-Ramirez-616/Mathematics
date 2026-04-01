using UnityEngine;

public class MovingObject : DrawableObject
{
    public Vector3 Velocity = Vector3.zero;
    public float MaxVelocity = 50; 
    public float CollisionRadius = 10;
    public DrawableObject CollisionCircle;
    public bool willDrawCollision = false;
    public bool willScreenWarp = true; 

    public override void Initalize()
    {
        base.Initalize();
    }

    public override void Tick()
    {
        base.Tick();
        UpdatePostion();
        DrawCollision(); 
    }

    public void UpdatePostion()
    {
        if (Velocity.magnitude > MaxVelocity)
        {

        }
    }

    public void DrawCollision()
    {
        if (CollisionCircle != null)
        {
            CollisionCircle.PerformDraw = willDrawCollision;
            CollisionCircle.Position = Position;
            CollisionCircle.Scale = Scale; 
        }
    }

    public void CreateCollision(float Radius, DrawableGrid grid, int sceneIndex)
    {
        CollisionRadius = Radius;
        CollisionCircle = DrawingTools.CreateCircleObject(Vector3.zero, Radius, 36, Color.magenta);
        grid.AddObjectToScene(sceneIndex, CollisionCircle);
    }

    public bool CheckForCollisionWith(MovingObject other)
    {
        return false; 
    }
}
