using UnityEngine;

public class DrawableParabola1 : DrawableObject
{
    public override void Initalize()
    {
        int X = 1; 

        for (X = -100; X < 100; X++)
        {
           // Debug.Log("PARABOLA 1 - Line: " + X);
            AddLineToObject(GetParbolaPointAt(X), GetParbolaPointAt(X + 1), Color.red);

        }

       // Debug.Log("PARABOLA 1 - Line Count: " + LineList.Count);

    }
      


    public float GetParbolaYAt(float num)
    {
        return Mathf.Pow(num, 2);

    }

    public Vector2 GetParbolaPointAt(float numX)
    {
        return new Vector2(numX, GetParbolaYAt(numX)); 

    }

}
