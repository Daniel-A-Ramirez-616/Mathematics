using UnityEngine;

public class ShipBThrust : DrawableObject
{
    public override void Initalize()
    {
      
        AddLineToObject(new Vector3(-10.25f, 2, 0), new Vector3(-20, 3, 0), Color.magenta);
        AddLineToObject(new Vector3(-10.25f, -2, 0), new Vector3(-20, -3, 0), Color.magenta);
    } 
}
