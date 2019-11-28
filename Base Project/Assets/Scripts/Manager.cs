using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    public Animator coordinatesButton;
    public Animator controlsButton;
    public Animator exitButton;
    public Animator coordinatesPanel;

    public void OpenCoordinatesPanel()
    {
        exitButton.SetBool("Enabled", false);
        controlsButton.SetBool("Enabled", false);
        coordinatesPanel.SetBool("Enabled", true);
        Debug.Log("Active Coordinates Button");
    }

    public void OpenControlsPanel()
    {
        coordinatesButton.SetBool("Enabled", false);
        exitButton.SetBool("Enabled", false);
        Debug.Log("Active Controls Button");
    }

    public void OpenExitPanel()
    {
        controlsButton.SetBool("Enabled", false);
        coordinatesButton.SetBool("Enabled", false);
        Debug.Log("Active Exit Button");
    }

    public void CloseCoordinatesPanel()
    {
        controlsButton.SetBool("Enabled", true);
        exitButton.SetBool("Enabled", true);
        coordinatesPanel.SetBool("Enabled", false);
        Debug.Log("Active Exit Button");
    }
}
