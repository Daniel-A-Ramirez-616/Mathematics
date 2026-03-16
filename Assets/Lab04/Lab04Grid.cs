using UnityEngine;

public class Lab04Grid : DrawableGrid
{
    public override void SetupScenes()
    {
        int sceneIndex;
        DrawableObject newGraph;


        AddScene("Empty Scene, Use Tab To Switch Scenes");


        sceneIndex = AddScene("Diamond As is (it's really small) ");
        newGraph = new DrawableDiamond();
        AddObjectToScene(sceneIndex, newGraph);


        sceneIndex = AddScene("Diamond at Scale of 20");
        newGraph = new DrawableDiamond();
        newGraph.Scale = (Vector3.one * 20f);
        AddObjectToScene(sceneIndex, newGraph);


        sceneIndex = AddScene("Diamond at Scale of 20,10");
        newGraph = new DrawableDiamond();
        newGraph.Scale = new Vector3(20, 10, 1);
        AddObjectToScene(sceneIndex, newGraph);


        sceneIndex = AddScene("Diamond at Scale of 20,10, Rotation of 45 deg. ");
        newGraph = new DrawableDiamond();
        newGraph.Scale = new Vector3(20, 10, 1);
        newGraph.Roation = (45 * Mathf.Deg2Rad);
        AddObjectToScene(sceneIndex, newGraph);

        sceneIndex = AddScene("Rotating Diamond  at Scale of 20,10");
        newGraph = new RotatingDiamond();
        newGraph.Scale = new Vector3(20, 10, 1);
        AddObjectToScene(sceneIndex, newGraph);

        sceneIndex = AddScene("Facing Box, At Scale of 10,10");
        newGraph = new FacingBox();
        newGraph.Scale = new Vector3(10, 10, 1);
        AddObjectToScene(sceneIndex, newGraph);

    }
}
