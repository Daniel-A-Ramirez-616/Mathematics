using UnityEngine;

public class RotatingDiamond : DrawableDiamond
{
    public float RotationRate = 90; 

    public override void Tick()
    {
        Roation += (RotationRate * Mathf.Deg2Rad * Time.deltaTime);
    }
}
