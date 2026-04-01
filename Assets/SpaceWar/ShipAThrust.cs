using UnityEngine;

public class ShipAThrust : DrawableObject
{
    public override void Initalize()
    {

        AddLineToObject(new Vector3(-4.25f, 2, 0), new Vector3(-14, 3, 0), Color.magenta);
        AddLineToObject(new Vector3(-4.25f, -2, 0), new Vector3(-14, -3, 0), Color.magenta);
    }
}
