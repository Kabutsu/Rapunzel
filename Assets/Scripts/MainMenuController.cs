using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    
    public Canvas controlsCanvas;
    public Canvas creditsCanvas;

    private bool disabled = false;

    // Use this for initialization
    void Start () {
        CloseMenu();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool MenuOpen()
    {
        return (controlsCanvas.enabled || creditsCanvas.enabled);
    }

    public void CloseMenu()
    {
        controlsCanvas.enabled = false;
        creditsCanvas.enabled = false;
    }

    public void OpenMenu(string menuName)
    {
        if (!disabled)
        {
            switch (menuName.ToLower())
            {
                case "controls":
                    CloseMenu();
                    controlsCanvas.enabled = true;
                    break;
                case "credits":
                    CloseMenu();
                    creditsCanvas.enabled = true;
                    break;
                default:
                    break;
            }
        }
    }

    public void Disable()
    {
        disabled = true;
        CloseMenu();
    }
}
