using UnityEngine;

/// <summary>
/// Responsible for moving the player
/// </summary>
public class PhysicsPlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Horizontal axis defined in Edit -> Project Settings -> Input Manager
    /// </summary>
    [SerializeField] private string m_HorizontalAxis;

    /// <summary>
    /// Vertical axis defined in Edit -> Project Settings -> Input Manager
    /// </summary>
    [SerializeField] private string m_VerticalAxis;

    /// <summary>
    /// Movement speed of the player
    /// </summary>
    [SerializeField] private float m_MovementSpeed;

    /// <summary>
    /// Rotation speed of the player
    /// </summary>
    [SerializeField] private float m_RotationSpeed;

    /// <summary>
    /// Reference to the rigidbody component, for physics interactions
    /// </summary>	
    private Rigidbody m_Rigidbody;
    
    /// <summary>
    /// Movement direction from the last frame
    /// </summary>
    private Vector3 m_Movement;

    void Awake()
    {
        // We get the component from the same GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get movement based on horizontal & vertical axis
        m_Movement = new Vector3(Input.GetAxis(m_HorizontalAxis), 0, Input.GetAxis(m_VerticalAxis));

        // Clamp diagonal movement
        if (m_Movement.magnitude > 1)
            m_Movement = m_Movement.normalized;

        // If we moved a bit, update rotation as well
        if (!Mathf.Approximately(m_Movement.magnitude, 0))
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(m_Movement), m_RotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Method called at fixed time intervals, used for physics interactions
    /// </summary>
    void FixedUpdate()
    {
        // Apply force on fixed update
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * Time.fixedDeltaTime * m_MovementSpeed);
    }
}
