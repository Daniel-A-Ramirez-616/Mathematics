using UnityEngine;
using TMPro; 

public class ShowSceneName : MonoBehaviour
{
    public DrawableGrid grid;

    public TextMeshProUGUI WhiteTextField;
    public TextMeshProUGUI BlackTextField;


    // Update is called once per frame
    void Update()
    {
        WhiteTextField.text = grid.GetCurrentSceneName();
        BlackTextField.text = grid.GetCurrentSceneName();
    }
}
