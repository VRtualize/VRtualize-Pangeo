using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes in the keyboard input and moves the camera respectively.
/// WSAD: Forward, backward, left, right
/// Space: Ascend
/// Left CTRL: Descend
/// Mouse: Rotate Camera
/// </summary>
public class KeyboardMovement : MonoBehaviour
{
    float speed = 20.0f;       // Default speed
    float sensitivity = 0.25f;  // Mouse sensitivity
    private Vector3 centerMouse = new Vector3(255, 255, 255);   // Place mouse in the middle of the screen rather than at the top
    private float totalRun = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Repositions to the new position after taking in keyboard and mouse inputs.
    /// </summary>
    void Update()
    {
        centerMouse = Input.mousePosition - centerMouse;
        centerMouse = new Vector3(-centerMouse.y * sensitivity, centerMouse.x * sensitivity, 0);
        centerMouse = new Vector3(transform.eulerAngles.x + centerMouse.x, transform.eulerAngles.y + centerMouse.y, 0);
        transform.eulerAngles = centerMouse;
        centerMouse = Input.mousePosition;
        
        // Keyboard commands
        Vector3 p = GetBaseInput();

        totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
        p = p * speed;
        
        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;

        transform.Translate(p);
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
        {
            newPosition.y = transform.position.y;
        }
        
        transform.position = newPosition;
        Globals.position = newPosition;
    }
   
    /// <summary>
    /// Finds the velocity of all of the keyboard inputs.
    /// </summary>
    /// <returns name="p_Velocity">Velocity of the new movement that will be applied to the current position</returns>
    private Vector3 GetBaseInput()
    { 
        //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            p_Velocity += new Vector3(0, 1, 0);
        }

        return p_Velocity;
    }
}
