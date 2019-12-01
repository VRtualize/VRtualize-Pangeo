/* SceneHandler.cs*/
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
    public GameObject eventSystem;


    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name.Contains("Button"))
        {
            Debug.Log("Button was clicked");

            button = e.target.gameObject.GetComponent<Button>();
            button.Select();
            button.onClick.Invoke();
        }
        else if (e.target.name.Contains("InputField"))
        {
            Debug.Log("InputField was clicked");
            field = e.target.gameObject.GetComponent<InputField>();
            field.Select();
            //field.ActivateInputField();
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name.Contains("Button"))
        {
            Debug.Log("Button is selected");

            button = e.target.gameObject.GetComponent<Button>();
            button.Select();
        }
        else if (e.target.name.Contains("InputField"))
        {
            Debug.Log("InputField is selected");
            //field = e.target.gameObject.GetComponent<InputField>();
            //field.Select();
        }

    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name.Contains("Button"))
        {
            Debug.Log("Button is deselected");

            button = e.target.gameObject.GetComponent<Button>();
        }
        else if (e.target.name.Contains("InputField"))
        {
            Debug.Log("InputField is deselected");
            field = e.target.gameObject.GetComponent<InputField>();
        }

        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
