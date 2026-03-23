using UnityEngine;

public struct TriangleData 
{
    public Vector3 PointA;
    public Vector3 PointB;
    public Vector3 PointC;

    public static TriangleData MakeTriangle(Vector3 pointA, Vector3 pointB, Vector3 pointC)
    {
        TriangleData result = new TriangleData();
        result.PointA = pointA;
        result.PointB = pointB;
        result.PointC = pointC;
        return result;
    }
}
