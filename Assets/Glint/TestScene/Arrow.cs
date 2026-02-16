using System;
using System.Collections.Generic;
using UnityEngine;



class Arrow : DrawingObject
{

    public override void Initalize()
    {
        Lines.Add(new Line(new Vector2(0, 10), new Vector2(0, 20), Color.red));
        Lines.Add(new Line(new Vector2(0, 20), new Vector2(20, 0), Color.red));
        Lines.Add(new Line(new Vector2(20, 0), new Vector2(0, -20), Color.red));
        Lines.Add(new Line(new Vector2(0, -20), new Vector2(0, -10), Color.red));
        Lines.Add(new Line(new Vector2(0, -10), new Vector2(-20, -10), Color.red));
        Lines.Add(new Line(new Vector2(-20, -10), new Vector2(-20, 10), Color.red));
        Lines.Add(new Line(new Vector2(-20, 10), new Vector2(0, 10), Color.red));

    }
}

