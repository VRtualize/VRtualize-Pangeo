/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

/// <summary>
/// SceneHandler class handles the pointer actions relating to the Main Menu.
/// </summary>
public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public Button button;
    public InputField field;
    public GameObject eventSystem;

    /// <summary>
    /// Controller movement that calculates current pointer position on the screen.
    /// </summary>
    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    /// <summary>
    /// Handles pointer clicks on the menu items.
    /// </summary>
    /// <param name="sender">Source of the click.</param>
    /// <param name="e">Event that triggered the click.</param>
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
        }
    }

    /// <summary>
    /// Handles events that pointer comes inside the boundary of an object.
    /// Buttons are highlighted when hovered over.
    /// </summary>
    /// <param name="sender">Source of the pointer.</param>
    /// <param name="e">Event that pointer enters an object.</param>
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
        }
    }

    /// <summary>
    /// Handles events that pointer exits the boundary of an object.
    /// Buttons will lose it's highlight when the pointer is no longer in the boundary.
    /// </summary>
    /// <param name="sender">Source of the pointer.</param>
    /// <param name="e">Event that pointer enters an object.</param>
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
