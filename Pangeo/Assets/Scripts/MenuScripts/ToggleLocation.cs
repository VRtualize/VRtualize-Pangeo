using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLocation : MonoBehaviour
{
    GameObject LocDisplay;
    bool active;

    // Start is called before the first frame update
    void Start()
    {
        LocDisplay = GameObject.Find("LocationCanvas");
        active = false;
        LocDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            if(active)
            {
                active = false;
                LocDisplay.SetActive(false);
            }
            else
            {
                active = true;
                LocDisplay.SetActive(true);
            }
        }
    }
}
