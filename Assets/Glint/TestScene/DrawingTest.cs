using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawingTest : MonoBehaviour
{

	void Update()
    {
		Vector3 mousePosition = Vector3.zero;

		Mouse mouse = Mouse.current; 
		if (mouse != null)
        {
			mousePosition = mouse.position.ReadValue(); 
        }

		Glint.AddCommand(new Line(new Vector3(0,			0,				0), mousePosition, Color.gray));
		Glint.AddCommand(new Line(new Vector3(Screen.width, 0,				0), mousePosition, Color.red));
		Glint.AddCommand(new Line(new Vector3(0,			Screen.height,	0), mousePosition, Color.green));
		Glint.AddCommand(new Line(new Vector3(Screen.width, Screen.height,	0), mousePosition, Color.blue));
	}
}
