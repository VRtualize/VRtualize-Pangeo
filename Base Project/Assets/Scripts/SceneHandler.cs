using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public Button button;
    public InputField field;
    public bool highlight = true;
    private bool isSelected;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "CoordinatesButton")
        {
            Debug.Log("Cube was clicked");
        }
        else if (e.target.name == "SettingsButton")
        {
            Debug.Log("Button was clicked");
        }

        button = e.target.gameObject.GetComponent<Button>();
        button.Select();
        button.onClick.Invoke();

        //field = e.target.gameObject.GetComponent<InputField>();
        //field.Select();
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "CoordinatesButton")
        {
            Debug.Log("Cube was entered");
        }
        else if (e.target.name == "SettingsButton")
        {
            Debug.Log("Button was entered");
        }

        //button = e.target.gameObject.GetComponent<Button>();
        //button.Select();

        field = e.target.gameObject.GetComponent<InputField>();
        //field.ActivateInputField();
        field.Select();
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "CoordinatesButton")
        {
            Debug.Log("Cube was exited");
        }
        else if (e.target.name == "SettingsButton")
        {
            Debug.Log("Button was exited");
        }
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }
}
