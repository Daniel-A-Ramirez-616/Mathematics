using UnityEngine;

public class DrawableDiamond : DrawableObject
{
    public override void Initalize()
    {
        AddLineToObject(new Vector3(1, 0, 0), new Vector3(0, 1, 0), Color.magenta);
        AddLineToObject(new Vector3(0, 1, 0), new Vector3(-1, 0, 0), Color.magenta);
        AddLineToObject(new Vector3(-1, 0, 0), new Vector3(0, -1, 0), Color.magenta);
        AddLineToObject(new Vector3(0, -1, 0), new Vector3(1, 0, 0), Color.magenta);

    }

}
