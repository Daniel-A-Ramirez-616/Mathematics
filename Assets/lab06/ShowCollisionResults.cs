using UnityEngine;
using TMPro; 

public class ShowCollisionResults : MonoBehaviour
{
    public Lab06Grid grid;

    public TextMeshProUGUI WhiteTextField;
    public TextMeshProUGUI BlackTextField;


    // Update is called once per frame
    void Update()
    {
        string result = "";
        result += grid.CircleCollisionResult + ": Circle" + "\n";
        result += grid.RectangleCollisionResult + ": Rectangle" +  "\n";
        result += grid.TriangleCollisionResult + ": Triangle" + "\n";


        WhiteTextField.text = result;
        BlackTextField.text = result;
    }
}
