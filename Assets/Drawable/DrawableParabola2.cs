using UnityEngine;

public class DrawableParabola2 : DrawableObject
{
    public override void Initalize()
    {
        int X = 1;

        for (X = -100; X < 100; X++)
        {
            AddLineToObject(GetParbolaPointAt(X), GetParbolaPointAt(X + 1), Color.red);

        }
    }

    public float GetParbolaYAt(float num)
    {
        return Mathf.Pow(num, 2) + (2 * num) + 1;

    }

    public Vector2 GetParbolaPointAt(float numX)
    {
        return new Vector2(numX, GetParbolaYAt(numX));

    }

}
