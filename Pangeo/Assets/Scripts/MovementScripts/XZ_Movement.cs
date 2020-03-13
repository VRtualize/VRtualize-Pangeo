/*xz_movementScript.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Valve.VR;
using UnityEngine.VR;

/// <summary>
/// XZ_Movement class allows movement in the XZ plane with the use of the left
/// controller trackpad.
/// 
/// Up - forward
/// Down - backward
/// Left - left
/// Right - right
/// </summary>
public class XZ_Movement : MonoBehaviour
{
    // Variable for the xz_movement actionset
    public SteamVR_ActionSet m_ActionSet;
    // Touch position on the controller
    public SteamVR_Action_Vector2 m_TouchPosition;

    private CharacterController m_CharacterController = null;

    // Location values for the player position and the head position
    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    // Sets the deadzone for how far the player needs to move their thumb before
    // activating a movement
    public Vector2 m_deadzone = new Vector2(0.1f, 0.1f);
    public Vector2 m_NeutralPosition = new Vector2(0.0f, 0.0f);
    public GameObject AxisHand;

    float speed = 50.0f;       // Default speed

    /// <summary>
    /// Initialize the character controller.
    /// </summary>
    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Initialize the the camera rig and head to be tracked.
    /// </summary>
    void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }

    /// <summary>
    /// Update the player location by reading in the touch position of the trackpad
    /// with the controller rotation angle.
    /// </summary>
    void Update()
    {
        Quaternion handRotation = Quaternion.identity;      // Create new quaternion with no rotation
        handRotation = AxisHand.transform.localRotation;    // Check the orientation of the controller

        // Adjust the orientation of the player
        Vector3 newRotation = new Vector3(0, transform.eulerAngles.y + handRotation.y, 0);
        Debug.Log("Rotation: " + newRotation);

        Vector3 p = GetBaseInput();
        Vector2 trackpad = new Vector2(p.x, 0);
        Vector2 trackpad2 = new Vector2(0, p.z);
        Vector3 moveDirection = Quaternion.AngleAxis(Vector2.Angle(trackpad, trackpad2) + AxisHand.transform.localRotation.eulerAngles.y, Vector3.up) * p;

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    /// <summary>
    /// Gets the touch position on the trackpad and convert them to a vector to
    /// map movement direction.
    /// </summary>
    /// <returns>velocity of player movement</returns>
    private Vector3 GetBaseInput()
    {
        Vector2 delta = m_TouchPosition[SteamVR_Input_Sources.LeftHand].axis;
        Debug.Log(delta.x + " " + delta.y);

        Vector3 p_Velocity = new Vector3(-delta.y, 0, delta.x);

        return p_Velocity;
    }
}
