using UnityEngine;

public class FacingBox : DrawableObject
{
    public bool isMoving = true;
    public bool isFacingMouse = true;
    public float MoveSpeed = 5f;

    public override void Initalize()
    {
        AddLineToObject(new Vector3(1, 1, 0), new Vector3(-1, 1, 0), Color.magenta);
        AddLineToObject(new Vector3(-1, 1, 0), new Vector3(-1, -1, 0), Color.magenta);
        AddLineToObject(new Vector3(-1, -1, 0), new Vector3(1, -1, 0), Color.magenta);
        AddLineToObject(new Vector3(1, -1, 0), new Vector3(1, 1, 0), Color.magenta);
        AddLineToObject(new Vector3(0, 0, 0), new Vector3(1, 0, 0), Color.magenta);


    }

    public override void Tick()
    {
        if (isMoving)
        {
            Position.x += MoveSpeed * Time.deltaTime;
        }

        if (isFacingMouse)
        {
            // This works because they're in Screen Space 
            //Vector3 screenPosition = DrawableGrid.Instance.origin;
            Vector3 screenPosition = DrawableGrid.Instance.GridToScreen(Position);
            Roation = V3ToAngle(screenPosition, DrawableGrid.Instance.MousePosition);
        }
    }


}
