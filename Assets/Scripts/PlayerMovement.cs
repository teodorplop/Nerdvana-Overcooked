using UnityEngine;

/// <summary>
/// Responsible for moving the player
/// </summary>
public class PlayerMovement : MonoBehaviour
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
	/// Reference to the character controller, component used to move a character and handle collisions
	/// </summary>	
	private CharacterController m_CharacterController;
	
	void Awake() 
	{
		// We get the component from the same GameObject
		m_CharacterController = GetComponent<CharacterController>();
	}
	
	void Update() 
	{
		// Get movement based on horizontal & vertical axis
		Vector3 movement = new Vector3(Input.GetAxis(m_HorizontalAxis), 0, Input.GetAxis(m_VerticalAxis));
		
		// Clamp diagonal movement
		if (movement.magnitude > 1) 
			movement = movement.normalized;
		
		// Move the player
		m_CharacterController.Move(movement * m_MovementSpeed * Time.deltaTime);
		
		// If we moved a bit, update rotation as well
		if (!Mathf.Approximately(movement.magnitude, 0))
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement), m_RotationSpeed * Time.deltaTime);
	}
}
