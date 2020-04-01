using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// KeyboardMenu opens the Main Menu screen when the 'ESC' key is hit.
/// </summary>
public class KeyboardMenu : MonoBehaviour
{
    bool active;

    /// <summary>
    /// Initializes the menu active, dependent on the current scene.
    /// </summary>
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.IndexOf("MainMenu") == -1)
        {
            active = false;
        }
        else
        {
            active = true;
        }
    }

    /// <summary>
    /// If the 'ESC' key is pressed, the Main Menu scene is launched.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Pressed E");

            if (!active)
            {
                active = true;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
