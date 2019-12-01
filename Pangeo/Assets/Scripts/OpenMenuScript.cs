using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Valve.VR;

public class OpenMenuScript : MonoBehaviour
{
    public SteamVR_Action_Boolean OpenMenu;
    public SteamVR_Input_Sources handType;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Activating menu");

        OpenMenu.AddOnStateUpListener(MenuInactive, handType);
        OpenMenu.AddOnStateDownListener(MenuActive, handType);
    }

    public void MenuActive(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Activating menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void MenuInactive(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Deactivating menu");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
