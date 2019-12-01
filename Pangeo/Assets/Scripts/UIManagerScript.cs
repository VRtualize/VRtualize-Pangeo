﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public Animator coordinatesButton;
    public Animator controlsButton;
    public Animator exitButton;
    public Animator background;

    public InputField longitudeInputField;
    public InputField latitudeInputField;

    bool fieldCoordinate;

    public void OpenCoordinatesPanel()
    {
        exitButton.SetBool("Enabled", false);
        controlsButton.SetBool("Enabled", false);
        background.SetBool("Enabled", true);
        Debug.Log("Active Coordinates Button");
    }

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
    }

    public void EnterCoordinates(Button key)
    {
        longitudeInputField = GameObject.Find("LongitudeInputField").GetComponent<InputField>();
        latitudeInputField = GameObject.Find("LatitudeInputField").GetComponent<InputField>();

        if (fieldCoordinate)
        {
            Debug.Log(latitudeInputField.text);
            latitudeInputField.text += key.GetComponentInChildren<Text>().text;
            Debug.Log(latitudeInputField.text);
        }
        else
        {
            Debug.Log(longitudeInputField.text);
            longitudeInputField.text += key.GetComponentInChildren<Text>().text;
            Debug.Log(longitudeInputField.text);
        }
    }

    public void GoToCoordinates()
    {
        Debug.Log("Going to Coordinates");
        SceneManager.LoadScene("World");
    }

    public void CloseCoordinatesPanel()
    {
        controlsButton.SetBool("Enabled", true);
        exitButton.SetBool("Enabled", true);
        background.SetBool("Enabled", false);
        Debug.Log("Close Coordinates Panel");
    }

    public void OpenControlsPanel()
    {
        coordinatesButton.SetBool("Enabled", false);
        exitButton.SetBool("Enabled", false);
        background.SetBool("ControlsPanelEnabled", true);
        Debug.Log("Active Controls Button");
    }

    public void CloseControlsPanel()
    {
        coordinatesButton.SetBool("Enabled", true);
        exitButton.SetBool("Enabled", true);
        background.SetBool("ControlsPanelEnabled", false);
        Debug.Log("Close Controls Panel");
    }

    public void OpenExitPanel()
    {
        Debug.Log("Active Exit Button");
        controlsButton.SetBool("Enabled", false);
        coordinatesButton.SetBool("Enabled", false);
        Application.Quit();
    }

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
