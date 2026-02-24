using UnityEngine;

public class DrawableParabola3 : DrawableObject
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
        return (-2 * Mathf.Pow(num, 2)) + (10 * num) + 12;

    }

    public Vector2 GetParbolaPointAt(float numX)
    {
        return new Vector2(numX, GetParbolaYAt(numX));

    }

}
