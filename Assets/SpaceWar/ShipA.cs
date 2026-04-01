using UnityEngine;

public class ShipA : DrawableObject
{
    public override void Initalize()
    {
        // Inner Circle
        AddLineToObject(new Vector3(2, 3, 0), new Vector3(-2, 3, 0), Color.red);
        AddLineToObject(new Vector3(-2, 3, 0), new Vector3(-3, 0, 0), Color.red);
        AddLineToObject(new Vector3(-3, 0, 0), new Vector3(-2, -3, 0), Color.red);
        AddLineToObject(new Vector3(-2, -3, 0), new Vector3(2, -3, 0), Color.red);
        AddLineToObject(new Vector3(2, -3, 0), new Vector3(3, 0, 0), Color.red);
        AddLineToObject(new Vector3(3, 0, 0), new Vector3(2, 3, 0), Color.red);

        // Back Engines 
        AddLineToObject(new Vector3(-2, -3, 0), new Vector3(-6, -9, 0), Color.red);
        AddLineToObject(new Vector3(-2, 3, 0), new Vector3(-6, 9, 0), Color.red);

        // Center lines 
        AddLineToObject(new Vector3(3, 0, 0), new Vector3(10, 0, 0), Color.red);
        AddLineToObject(new Vector3(2, 3, 0), new Vector3(9, 3, 0), Color.red);
        AddLineToObject(new Vector3(2, -3, 0), new Vector3(9,-3, 0), Color.red);

        // Outer Circle 
       
        AddLineToObject(new Vector3(6, 9, 0), new Vector3(0, 10, 0), Color.red);
        AddLineToObject(new Vector3(0, 10, 0), new Vector3(-6, 9, 0), Color.red);
       
        AddLineToObject(new Vector3(-6, -9, 0), new Vector3(0, -10, 0), Color.red);
        AddLineToObject(new Vector3(0, -10, 0), new Vector3(6, -9, 0), Color.red);
        
        AddLineToObject(new Vector3(6, -9, 0), new Vector3(9, -3, 0), Color.red);
        
        AddLineToObject(new Vector3(9, -3, 0), new Vector3(10, 0, 0), Color.red);
        AddLineToObject(new Vector3(10, 0, 0), new Vector3(9, 3, 0), Color.red);
        AddLineToObject(new Vector3(9, 3, 0), new Vector3(6, 9, 0), Color.red);

    }
}
