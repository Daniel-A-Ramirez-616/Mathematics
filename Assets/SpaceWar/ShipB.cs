using UnityEngine;

public class ShipB : DrawableObject
{
    public override void Initalize()
    {
        // Inner Circle
        AddLineToObject(new Vector3(2, 3, 0), new Vector3(-2, 3, 0), Color.cyan);
        AddLineToObject(new Vector3(-2, 3, 0), new Vector3(-3, 0, 0), Color.cyan);
        AddLineToObject(new Vector3(-3, 0, 0), new Vector3(-2, -3, 0), Color.cyan);
        AddLineToObject(new Vector3(-2, -3, 0), new Vector3(2, -3, 0), Color.cyan);
        AddLineToObject(new Vector3(2, -3, 0), new Vector3(3, 0, 0), Color.cyan);
        AddLineToObject(new Vector3(3, 0, 0), new Vector3(2, 3, 0), Color.cyan);

        // Core 
        AddLineToObject(new Vector3(-2, 3, 0), new Vector3(-6, 9, 0), Color.cyan);
        AddLineToObject(new Vector3(-2, -3, 0), new Vector3(-6, -9, 0), Color.cyan);
        
        AddLineToObject(new Vector3(2, 3, 0), new Vector3(8, 9, 0), Color.cyan);
        AddLineToObject(new Vector3(2, -3, 0), new Vector3(8, -9, 0), Color.cyan);


        // Center lines 
        AddLineToObject(new Vector3(-3, 0, 0), new Vector3(-8, 0, 0), Color.cyan);
        AddLineToObject(new Vector3(-2, 3, 0), new Vector3(-9, 3, 0), Color.cyan);
        AddLineToObject(new Vector3(-2, -3, 0), new Vector3(-9, -3, 0), Color.cyan);

        // Outer Circle 

        AddLineToObject(new Vector3(-6, 9, 0), new Vector3(0, 10, 0), Color.cyan);
        AddLineToObject(new Vector3(0, 10, 0), new Vector3(8, 9, 0), Color.cyan);

        AddLineToObject(new Vector3(8, -9, 0), new Vector3(0, -10, 0), Color.cyan);
        AddLineToObject(new Vector3(0, -10, 0), new Vector3(-6, -9, 0), Color.cyan);

        AddLineToObject(new Vector3(-6, -9, 0), new Vector3(-9, -3, 0), Color.cyan);

        AddLineToObject(new Vector3(-9, -3, 0), new Vector3(-8, 0, 0), Color.cyan);
        AddLineToObject(new Vector3(-8, 0, 0), new Vector3(-9, 3, 0), Color.cyan);
        AddLineToObject(new Vector3(-9, 3, 0), new Vector3(-6, 9, 0), Color.cyan);

        

    }
}
