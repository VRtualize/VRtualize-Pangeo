using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// UIManagerScript class handles the actions with the menu.
/// </summary>
public class UIManagerScript : MonoBehaviour
{
    public Animator coordinatesButton;
    public Animator controlsButton;
    public Animator exitButton;
    public Animator background;

    public InputField longitudeInputField;
    public InputField latitudeInputField;
    public GameObject eventSystem;

    bool fieldCoordinate;

    /// <summary>
    /// Opens the Coordinates Panel.
    /// </summary>
    public void OpenCoordinatesPanel()
    {
        exitButton.SetBool("Enabled", false);
        controlsButton.SetBool("Enabled", false);
        background.SetBool("Enabled", true);
        background.SetBool("KeyboardEnabled", false);

        Debug.Log("Active Coordinates Button");
    }

    /// <summary>
    /// Selects an Input Field and opens the Numerical Keypad.
    /// </summary>
    /// <param name="field">Input field to be selected.</param>
    public void SelectInputField(InputField field)
    {
        background.SetBool("KeyboardEnabled", true);
        if (field.name == "LongitudeInputField")
        {
            fieldCoordinate = false;
            Debug.Log("Input Field: " + field.name);
        }
        else if (field.name == "LatitudeInputField")
        {
            fieldCoordinate = true;
            Debug.Log("Input Field: " + field.name);
        }
        Debug.Log(field.name);
    }

    /// <summary>
    /// Handles the input from the virtual keypad.
    /// </summary>
    /// <param name="key">Key on the keypad.</param>
    public void EnterCoordinates(Button key)
    {
        if (fieldCoordinate)
        {
            Debug.Log(latitudeInputField.text.GetType());
            if (key.name == "ButtonBack")
            {
                latitudeInputField.text = this.TrimLastCharacter(latitudeInputField.text);
            }
            else
            {
                latitudeInputField.text += key.GetComponentInChildren<Text>().text;
                Debug.Log(latitudeInputField.text);
            }
        }

        else
        {
            if (key.name == "ButtonBack")
            {
                longitudeInputField.text = this.TrimLastCharacter(longitudeInputField.text);
            }
            else
            {
                Debug.Log(longitudeInputField.text);
                longitudeInputField.text += key.GetComponentInChildren<Text>().text;
                Debug.Log(longitudeInputField.text);
            }
        }
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    /// <summary>
    /// Deletes the last character on the input field.
    /// </summary>
    /// <param name="str">Current string in the input field.</param>
    /// <returns></returns>
    public string TrimLastCharacter(string str)
    {
        if(string.IsNullOrEmpty(str))
        {
            return str;
        }
        else
        {
            return str.Remove(str.Length - 1);
        }
    }

    /// <summary>
    /// Loads the World scene with the specified coordinates.
    /// </summary>
    public void GoToCoordinates()
    {
        Debug.Log("Going to Coordinates");
        double longitude = Convert.ToDouble(longitudeInputField.text);
        double latitude = Convert.ToDouble(latitudeInputField.text);

        Globals.latitude = latitude;
        Globals.longitude = longitude;

        Debug.Log("Current Latitude " + Globals.latitude);
        Debug.Log("Current Longitude " + Globals.longitude);

        SceneManager.LoadScene("World");
    }

    /// <summary>
    /// Closes the Coordinates Panel.
    /// </summary>
    public void CloseCoordinatesPanel()
    {
        Debug.Log("Close Coordinates Panel");

        background.SetBool("Enabled", false);
        background.SetBool("KeyboardEnabled", false);
        controlsButton.SetBool("Enabled", true);
        exitButton.SetBool("Enabled", true);
    }

    /// <summary>
    /// Opens the Controls Panel.
    /// </summary>
    public void OpenControlsPanel()
    {
        coordinatesButton.SetBool("Enabled", false);
        exitButton.SetBool("Enabled", false);
        background.SetBool("ControlsPanelEnabled", true);
        background.SetBool("Enabled", false);
        Debug.Log("Active Controls Button");
    }

    /// <summary>
    /// Closes the Controls Panel.
    /// </summary>
    public void CloseControlsPanel()
    {
        Debug.Log("Close Controls Panel");

        coordinatesButton.SetBool("Enabled", true);
        exitButton.SetBool("Enabled", true);
        background.SetBool("ControlsPanelEnabled", false);
    }

    /// <summary>
    /// Opens the Exit Panel.
    /// </summary>
    public void OpenExitPanel()
    {
        Debug.Log("Active Exit Button");
        controlsButton.SetBool("Enabled", false);
        coordinatesButton.SetBool("Enabled", false);
        Application.Quit();
    }

    /// <summary>
    /// Handles the animations when the pointer hovers over a button.
    /// </summary>
    /// <param name="button">Button hovered over.</param>
    public void OnPointerEnter(Button button)
    {
        Debug.Log("Entering " + button.name);
        if (button.name == "CoordinatesButton")
        {
            coordinatesButton.SetBool("Hover", true);
        }
        else if (button.name == "ControlsButton")
        {
            controlsButton.SetBool("Hover", true);
        }
        else if (button.name == "ExitButton")
        {
            exitButton.SetBool("Hover", true);
        }
    }

    /// <summary>
    /// Handles the animations when the pointer exits a button.
    /// </summary>
    /// <param name="button">Button that is exited.</param>
    public void OnPointerExit(Button button)
    {
        Debug.Log("Exiting " + button.name);
        if (button.name == "CoordinatesButton")
        {
            coordinatesButton.SetBool("Hover", false);
        }
        else if (button.name == "ControlsButton")
        {
            controlsButton.SetBool("Hover", false);
        }
        else if (button.name == "ExitButton")
        {
            exitButton.SetBool("Hover", false);
        }
    }
}
