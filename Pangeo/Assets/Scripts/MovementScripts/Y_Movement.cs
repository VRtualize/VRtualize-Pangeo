/*Y_Movement.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Valve.VR;
using UnityEngine.VR;

/// <summary>
/// Y_Movement class that allows the movement in the y axis with the use of
/// the right controller trackpad.
/// 
/// Up - ascend
/// Down - descend
/// </summary>
public class Y_Movement : MonoBehaviour
{
    //Variable for the y_movement actionset
    public SteamVR_ActionSet m_ActionSet;
    // Touch location on the right trackpad
    public SteamVR_Action_Vector2 m_TouchPosition;

    // Variable for storing the character controller
    private CharacterController m_CharacterController = null;

    // Location values for the player position and the head position
    private Transform m_CameraRig = null;
    private Transform m_Head = null;
    public GameObject Head;

    // Sets the deadzone for how far the player needs to move their thumb before
    // activating movement
    public Vector2 m_deadzone = new Vector2(0.1f, 0.1f);
    public Vector2 m_NeutralPosition = new Vector2(0.0f, 0.0f);

    float speed = 25.0f;       // Default speed

    /// <summary>
    /// Initialize the character controller.
    /// </summary>
    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }
    
    /// <summary>
    /// Initialize the camera rig and head for tracking.
    /// </summary>
    void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }

    /// <summary>
    /// Update the player location by reading in the touch position on the trackpad.
    /// </summary>
    void Update()
    {
        Vector3 p = GetBaseInput();

        if (Mathf.Abs(p.y) >= m_NeutralPosition.y + m_deadzone.y)
        {
            transform.Translate(p * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Gets the touch position on the trackpad and convert them to a vector to
    /// map movement direction.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetBaseInput()
    {
        Vector2 delta = m_TouchPosition[SteamVR_Input_Sources.RightHand].axis;
        Debug.Log(delta.y);

        Vector3 p_Velocity = new Vector3(0, delta.y, 0);

        return p_Velocity;
    }
}
