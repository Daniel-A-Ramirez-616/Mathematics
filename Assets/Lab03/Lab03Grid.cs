using UnityEngine;

public class Lab03Grid : DrawableGrid
{
    public override void SetupScenes()
    {
        int sceneIndex; 
        DrawableArrow newArrow;
        DrawableObject newObject;


        AddScene("Empty Scene, Use Tab To Switch Scenes");


        sceneIndex = AddScene("Arrow, As is");
        newArrow = new DrawableArrow();
        AddObjectToScene(sceneIndex, newArrow);


        sceneIndex = AddScene("Arrow, Moved Forward");
        newArrow = new DrawableArrow();
        newArrow.Position = new Vector2(30, 0);
        AddObjectToScene(sceneIndex, newArrow);


        sceneIndex = AddScene("Arrow, 50% size");
        newArrow = new DrawableArrow();
        newArrow.Scale = (Vector3.one * .5f); 
        AddObjectToScene(sceneIndex, newArrow);


        sceneIndex = AddScene("Arrow, Moved Forward at 25% size");
        newArrow = new DrawableArrow();
        newArrow.Position = new Vector2(30, 0);
        newArrow.Scale = (Vector3.one * .25f);
        AddObjectToScene(sceneIndex, newArrow);


        sceneIndex = AddScene("Parabola1");
        newObject = new DrawableParabola1();
        AddObjectToScene(sceneIndex, newObject);


        sceneIndex = AddScene("Parabola2");
        newObject = new DrawableParabola2();
        AddObjectToScene(sceneIndex, newObject);


        sceneIndex = AddScene("Parabola3");
        newObject = new DrawableParabola3();
        AddObjectToScene(sceneIndex, newObject);


        sceneIndex = AddScene("Parabola4");
        newObject = new DrawableParabola4();
        AddObjectToScene(sceneIndex, newObject);
    }
}
