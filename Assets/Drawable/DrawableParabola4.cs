using UnityEngine;

public class DrawableParabola4 : DrawableObject
{
    public override void Initalize()
    {

        //Y is the point on the parabola.
        int Y = 1;

        for (Y = -100; Y < 100; Y++)
        {
            AddLineToObject(GetParbolaPointAt(Y + 1), GetParbolaPointAt(Y), Color.red);

        }
    }

    public float GetParbolaXAt(float num)
    {
        return (-1 * Mathf.Pow(num, 3));

    }

    public Vector2 GetParbolaPointAt(float numY)
    {
        return new Vector2(GetParbolaXAt(numY), numY);

    }

}
