/*xz_movementScript.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Valve.VR;

/// <summary>
/// xz_MovementScript class that opens the menu with the click of the menu button.
/// </summary>
public class XZ_Movement : MonoBehaviour
{
    private Vector2 trackpad;
    private float Direction;
    private Vector3 moveDirection;


    public SteamVR_Input_Sources Hand; //Set Hand To Get Input From
    public float speed = 100.0f;
    public GameObject Head;
    public CapsuleCollider Collider;
    public GameObject AxisHand; //Hand Controller GameObject
    public float Deadzone; //the Deadzone of the trackpad. used to prevent unwanted walking.
                           // Start is called before the first frame update

    void Update()
    {
        //Set size and position of the capsule collider so it maches our head.
        Collider.height = Head.transform.localPosition.y;
        Collider.center = new Vector3(Head.transform.localPosition.x, Head.transform.localPosition.y / 2, Head.transform.localPosition.z);

        moveDirection = Quaternion.AngleAxis(Angle(trackpad) + AxisHand.transform.localRotation.eulerAngles.y, Vector3.up) * Vector3.forward;//get the angle of the touch and correct it for the rotation of the controller
        updateInput();

        if (GetComponent<Rigidbody>().velocity.magnitude < speed && trackpad.magnitude > Deadzone)
        {//make sure the touch isn't in the deadzone and we aren't going too fast.
            GetComponent<Rigidbody>().AddForce(moveDirection * 30);
        }
    }

    public static float Angle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

    private void updateInput()
    {
        trackpad = SteamVR_Actions._default.XZ_Movement.GetAxis(Hand);
    }

    ////Variable for grabbing the desired actionset
    //public SteamVR_ActionSet m_ActionSet;
    //// varaibles for storing if the touchpads have been used on the controller
    //public SteamVR_Action_Vector2 m_TouchPosition;
    //// varaible for storing player speed
    //private float m_Speed = 0.0f;
    //// varaible for storing the character controller
    //private CharacterController m_CharacterController = null;
    ////location values for the player position and the head position
    //private Transform m_CameraRig = null;
    //private Transform m_Head = null;


    ////sets the deadzone for how far the player needs to move their thumb before
    //public Vector2 m_deadzone = new Vector2(0.1f, 0.1f);
    //public Vector2 m_NeutralPosition = new Vector2(0.0f, 0.0f);

    //float speed = 1.0f;       // Default speed
    //private float totalRun = 1.0f;

    //private void Awake()
    //{
    //    m_CharacterController = GetComponent<CharacterController>();
    //}
    //// Start is called before the first frame update
    //void Start()
    //{
    //    m_CameraRig = SteamVR_Render.Top().origin;
    //    m_Head = SteamVR_Render.Top().head;
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    Quaternion newRotation = Quaternion.identity; // Create new quaternion with no rotation
    //    newRotation.x = transform.rotation.x; // Get only the X rotation from the controllers quaternion

    //    // Output the x rotation. It should be a value between -1 and 1
    //    Debug.Log("newRotation.x: " + newRotation.x);
    //    // Convert to a value between 0 and 360
    //    float xEuler = (newRotation.x + 1) * 180;
    //     // Output the x rotation as a Euler angle
    //     Debug.Log("xEuler: " + xEuler);

    //    #region Vector2 Action
    //    Vector3 p = GetBaseInput();

    //    totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
    //    p = p * speed;

    //    p = p * Time.deltaTime;
    //    Vector3 newPosition = transform.position;

    //    transform.Translate(p);
    //    newPosition.x = transform.position.x;
    //    newPosition.z = transform.position.z;

    //    transform.position = newPosition;

    //    //Vector2 delta = m_TouchPosition[SteamVR_Input_Sources.LeftHand].delta;
    //    ////Debug.Log(delta.x + " " + delta.y);
    //    //if (delta.x >= (m_NeutralPosition.x + m_deadzone.x) && delta.y >= (m_NeutralPosition.y + m_deadzone.y))
    //    //{
    //    //    Debug.Log("Player is moving right and up");
    //    //}
    //    #endregion
    //}

    //private Vector3 GetBaseInput()
    //{
    //    Vector2 delta = m_TouchPosition[SteamVR_Input_Sources.LeftHand].axis;

    //    //returns the basic values, if it's 0 than it's not active.
    //    Vector3 p_Velocity = new Vector3();
    //    if (delta.y >= (m_NeutralPosition.y + m_deadzone.y))
    //    {
    //        p_Velocity += new Vector3(0, 0, 1);
    //    }
    //    if (delta.y <= -(m_NeutralPosition.y + m_deadzone.y))
    //    {
    //        p_Velocity += new Vector3(0, 0, -1);
    //    }
    //    if (delta.x <= -(m_NeutralPosition.x + m_deadzone.x))
    //    {
    //        p_Velocity += new Vector3(-1, 0, 0);
    //    }
    //    if (delta.x >= (m_NeutralPosition.x + m_deadzone.x))
    //    {
    //        p_Velocity += new Vector3(1, 0, 0);
    //    }
    //    return p_Velocity;
    //}
}
