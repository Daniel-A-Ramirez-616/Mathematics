using UnityEngine;

public class Missle : MovingObject
{
    public float MoveSpeed = 50f;
    public float Timer = 5;
    float TimerReset = 0;

    public override void Initalize()
    {
        base.Initalize();

        AddLineToObject(new Vector3(2, 0, 0), new Vector3(-2, 2, 0), Color.yellow);
        AddLineToObject(new Vector3(-2, 2, 0), new Vector3(-1, 0, 0), Color.yellow);
        AddLineToObject(new Vector3(-1, 0, 0), new Vector3(-2, -2, 0), Color.yellow);
        AddLineToObject(new Vector3(-2, -2, 0), new Vector3(2, 0, 0), Color.yellow);

        TimerReset = Timer;
    }

    public override void Tick()
    {
          base.Tick();

        Timer -= Time.deltaTime;

        if(Timer <= 0)
        {
            SpaceWarGrid.self.RemoveObject(this);
            if (CollisionCircle != null)
            {
                SpaceWarGrid.self.RemoveObject(CollisionCircle);
            }
            Timer = TimerReset;
        }

        if (CheckForCollisionWith(SpaceWarGrid.self.ShipAObject))
        {
            Debug.Log("Hit Ship A");
            SpaceWarGrid.self.PlayerBScore++;
            SpaceWarGrid.self.RemoveObject(this);
            if (CollisionCircle != null)
            {
                SpaceWarGrid.self.RemoveObject(CollisionCircle);
            }
        }

        if (CheckForCollisionWith(SpaceWarGrid.self.ShipBObject))
        {
            Debug.Log("Hit Ship B");
            SpaceWarGrid.self.PlayerAScore++;
            SpaceWarGrid.self.RemoveObject(this);
            if (CollisionCircle != null)
            {
                SpaceWarGrid.self.RemoveObject(CollisionCircle);
            }

        }
    }

    public void MakeMissle(float angle, Vector3 SpawnPosition, DrawableGrid grid, int sceneIndex)
    {
        Missle missle = new Missle();
        missle.Position = SpawnPosition;
        //missle.SetRotationinDegrees(angle);
        missle.CreateCollision(2, grid, sceneIndex); 
        missle.willDrawCollision = true;
        missle.LaunchMissle(angle);
        SpaceWarGrid.self.AddObjectToScene(sceneIndex, missle);
        SpaceWarGrid.self.MovingObjectlist.Add(missle);
    }

    public void LaunchMissle(float angle)
    {
        SetRotationinDegrees(angle); 
        Velocity = DrawingTools.CircleRadiusPoint(Vector3.zero, angle, 1) * MoveSpeed; 
    }
}
