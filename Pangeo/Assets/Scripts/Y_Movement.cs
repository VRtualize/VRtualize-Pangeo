/*xz_movementScript.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Valve.VR;
using UnityEngine.VR;

/// <summary>
/// xz_MovementScript class that opens the menu with the click of the menu button.
/// </summary>
public class Y_Movement : MonoBehaviour
{
    //Variable for grabbing the desired actionset
    public SteamVR_ActionSet m_ActionSet;
    // varaibles for storing if the touchpads have been used on the controller
    public SteamVR_Action_Vector2 m_TouchPosition;
    // varaible for storing the character controller
    private CharacterController m_CharacterController = null;
    //location values for the player position and the head position
    private Transform m_CameraRig = null;
    private Transform m_Head = null;
    public GameObject Head;


    //sets the deadzone for how far the player needs to move their thumb before
    public Vector2 m_deadzone = new Vector2(0.1f, 0.1f);
    public Vector2 m_NeutralPosition = new Vector2(0.0f, 0.0f);

    float speed = 25.0f;       // Default speed

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = GetBaseInput();

        if (Mathf.Abs(p.y) >= m_NeutralPosition.y + m_deadzone.y)
        {
            transform.Translate(p * speed * Time.deltaTime);
        }
    }

    private Vector3 GetBaseInput()
    {
        Vector2 delta = m_TouchPosition[SteamVR_Input_Sources.RightHand].axis;
        Debug.Log(delta.y);

        Vector3 p_Velocity = new Vector3(0, delta.y, 0);

        return p_Velocity;
    }
}
