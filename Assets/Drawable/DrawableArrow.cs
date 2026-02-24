using System;
using System.Collections.Generic;
using UnityEngine;



class DrawableArrow : DrawableObject
{

    public override void Initalize()
    {
        AddLineToObject(new Vector2(0, 10), new Vector2(0, 20), Color.red);
        AddLineToObject(new Vector2(0, 20), new Vector2(20, 0), Color.red);
        AddLineToObject(new Vector2(20, 0), new Vector2(0, -20), Color.red);
        AddLineToObject(new Vector2(0, -20), new Vector2(0, -10), Color.red);
        AddLineToObject(new Vector2(0, -10), new Vector2(-20, -10), Color.red);
        AddLineToObject(new Vector2(-20, -10), new Vector2(-20, 10), Color.red);
        AddLineToObject(new Vector2(-20, 10), new Vector2(0, 10), Color.red);

    }
}

