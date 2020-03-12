using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    Button N;
    Button S;
    Button E;
    Button W;
    InputField longitude;
    InputField latitude;

    public void Start()
    {
        N = GameObject.Find("NorthButton").GetComponent<Button>();
        S = GameObject.Find("SouthButton").GetComponent<Button>();
        E = GameObject.Find("EastButton").GetComponent<Button>();
        W = GameObject.Find("WestButton").GetComponent<Button>();
        longitude = GameObject.Find("LongitudeInputField").GetComponent<InputField>();
        latitude = GameObject.Find("LatitudeInputField").GetComponent<InputField>();
    }

    public void OpenCoordinatesPanel()
    {
        Debug.Log("Open Coordinates Panel");
        SceneManager.LoadScene("MainMenu");
    }

    public void ActivateLatitudeInputField()
    {
        EnableNSHemisphere();
    }

    public void ActivateLongitudeInputField()
    {
        EnableEWHemisphere();
    }

    public void DisableEWHemisphere()
    {
        DisableCoordinate(E);
        DisableCoordinate(W);

        E.interactable = false;
        W.interactable = false;
    }

    public void DisableNSHemisphere()
    {
        DisableCoordinate(N);
        DisableCoordinate(S);

        N.interactable = false;
        S.interactable = false;
    }

    public void EnableEWHemisphere()
    {
        EnableCoordinate(E);
        EnableCoordinate(W);

        E.interactable = true;
        W.interactable = true;
    }

    public void EnableNSHemisphere()
    {
        EnableCoordinate(N);
        EnableCoordinate(S);

        N.interactable = true;
        S.interactable = true;
    }

    public void DisableCoordinate(Button button)
    {
        Transform trans = button.transform;
        Transform childTrans = trans.Find("Image");

        try
        {
            GameObject image = childTrans.gameObject;
            image.GetComponent<Image>().color = new Color32(147, 147, 147, 150);
        }
        catch { }
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

    public void MakeInputPositive(InputField field)
    {
        field.text = MakePositive(field.text);
    }

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
