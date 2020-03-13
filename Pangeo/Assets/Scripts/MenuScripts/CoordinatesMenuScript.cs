/*CoordinatesMenuScript.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// CoordinatesMenuScript maintains the functionality of adding hemisphere
/// information to the coordinates input fields.
/// </summary>
public class CoordinatesMenuScript : MonoBehaviour
{
    public Button N;
    public Button S;
    public Button E;
    public Button W;
    public InputField longitude;
    public InputField latitude;

    /// <summary>
    /// Initializes all hemisphere buttons
    /// </summary>
    public void Start()
    {
        /*N = GameObject.Find("NorthButton").GetComponent<Button>();
        S = GameObject.Find("SouthButton").GetComponent<Button>();
        E = GameObject.Find("EastButton").GetComponent<Button>();
        W = GameObject.Find("WestButton").GetComponent<Button>();
        longitude = GameObject.Find("LongitudeInputField").GetComponent<InputField>();
        latitude = GameObject.Find("LatitudeInputField").GetComponent<InputField>();*/
    }

    /// <summary>
    /// Opens the coordinates panel
    /// </summary>
    public void OpenCoordinatesPanel()
    {
        Debug.Log("Open Coordinates Panel");
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Activates the North and South Hemisphere buttons.
    /// </summary>
    public void ActivateLatitudeInputField()
    {
        EnableNSHemisphere();
        DisableEWHemisphere();
    }

    /// <summary>
    /// Enables the longitude input field
    /// </summary>
    public void ActivateLongitudeInputField()
    {
        EnableEWHemisphere();
        DisableNSHemisphere();
    }

    /// <summary>
    /// Disables the east and west hemisphere buttons
    /// </summary>
    public void DisableEWHemisphere()
    {
        DisableCoordinate(E);
        DisableCoordinate(W);

        E.interactable = false;
        W.interactable = false;
    }

    /// <summary>
    /// Disables the north and south hemisphere buttons
    /// </summary>
    public void DisableNSHemisphere()
    {
        DisableCoordinate(N);
        DisableCoordinate(S);

        N.interactable = false;
        S.interactable = false;
    }

    /// <summary>
    /// Enables the east and west hemisphere buttons
    /// </summary>
    public void EnableEWHemisphere()
    {
        EnableCoordinate(E);
        EnableCoordinate(W);

        E.interactable = true;
        W.interactable = true;
    }

    /// <summary>
    /// Enables the north and south hemisphere buttons
    /// </summary>
    public void EnableNSHemisphere()
    {
        EnableCoordinate(N);
        EnableCoordinate(S);

        N.interactable = true;
        S.interactable = true;
    }

    /// <summary>
    /// Disables a coordinate button
    /// </summary>
    /// <param name="button">button to be disabled</param>
    public void DisableCoordinate(Button button)
    {
        Transform trans = button.transform;
        Transform childTrans = trans.Find("Image");

        try
        {
            GameObject image = childTrans.gameObject;
            image.GetComponent<Image>().color = new Color32(147, 147, 147, 150);
        } catch { }

        button.interactable = false;
        button.enabled = false;
        button.GetComponent<Collider>().isTrigger = false;

        Debug.Log("Interactable? " + button.name + " : " + button.IsInteractable());
    }

    public void EnableCoordinate(Button button)
    {
        Transform trans = button.transform;
        Transform childTrans = trans.Find("Image");

        try
        {
            GameObject image = childTrans.gameObject;
            image.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } catch{}

        button.interactable = true;
        button.enabled = true;
        button.GetComponent<Collider>().isTrigger = true;

        Debug.Log("Interactable? " + button.name + " : " + button.IsInteractable());
    }

    public void MakeInputNegative(InputField field)
    {
        field.text = MakeNegative(field.text);
    }

    public string MakeNegative(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }
        else if (text.IndexOf("-") != -1)
        {
            return text;
        }
        else
        {
            return text.Insert(0, "-");
        }
    }

    /// <summary>
    /// Make the value in the input field positive.
    /// </summary>
    /// <param name="field"></param>
    public void MakeInputPositive(InputField field)
    {
        field.text = MakePositive(field.text);
    }

    /// <summary>
    /// Make the string positive
    /// </summary>
    /// <param name="text">positive string</param>
    /// <returns></returns>
    public string MakePositive(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }
        else if (text.IndexOf("-") != -1)
        {
            return text.Remove(0, 1);
        }
        else
        {
            return text;
        }
    }
}
