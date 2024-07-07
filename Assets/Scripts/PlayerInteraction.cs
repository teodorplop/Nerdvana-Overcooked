using UnityEngine;

/// <summary>
/// Responsible for handling interactions between player and objects. <br/>
/// Using raycast in front of the player to detect whether there's any object to interact with.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
	/// <summary>
	/// Size of the box that is being cast
	/// </summary>
	[Header("Raycast Settings")]
	[SerializeField] private Vector3 m_BoxCastSize;
	
	/// <summary>
	/// The length of the cast
	/// </summary>
	[SerializeField] private float m_CastLength;
	
	/// <summary>
	/// Maximum number of objects we detect in a cast
	/// </summary>
	[SerializeField] private int m_CastBufferSize;
	
	/// <summary>
	/// The layer used for objects that are interactable
	/// </summary>
	[SerializeField] private LayerMask m_InteractionLayer;
	
	/// <summary>
	/// Input for interacting, configured in Edit -> Project Settings -> Input Manager
	/// </summary>
	[Header("Input Settings")]
	[SerializeField] private string m_InteractInput;
	
	/// <summary>
	/// The interactable object we are looking at
	/// </summary>
	private InteractableObject m_CurrentInteractableObject;
	
	/// <summary>
	/// Array in which we store all objects we hit during a cast
	/// </summary>
	private RaycastHit[] m_CastBuffer;
	
	void Awake() 
	{
		// Initialize the cast array
		m_CastBuffer = new RaycastHit[m_CastBufferSize];
	}
	
	void Update() 
	{
		InteractableObject interactableObject = null;
		
		// Perform a cast
		int castCounter = Physics.BoxCastNonAlloc(transform.position, m_BoxCastSize / 2, transform.forward, m_CastBuffer, transform.rotation, m_CastLength, m_InteractionLayer.value);
		
		// If we hit any objects
		if (castCounter > 0)
		{
			// Search for any object that we can interact with
			while (castCounter > 0 && interactableObject == null) 
			{
				--castCounter;
				
				// Fetch the InteractableObject component from the object we hit
				interactableObject = m_CastBuffer[castCounter].collider.GetComponent<InteractableObject>();
				
				// If we hit an interactable object, make sure we are also allowed to interact with it
				if (interactableObject != null && !interactableObject.CanInteractWith(this))
					// If we're not allowed, let's remove it
					interactableObject = null;
			}
		}
		
		// If we hit a different object than the one we currently have
		if (interactableObject != m_CurrentInteractableObject) 
		{
			// Deselect the current one
			if (m_CurrentInteractableObject != null) 
				m_CurrentInteractableObject.Deselect();
			
			m_CurrentInteractableObject = interactableObject;
			
			// Select this one instead
			if (m_CurrentInteractableObject != null)
				m_CurrentInteractableObject.Select();
		}
		
		// If we have a selected object
		if (m_CurrentInteractableObject != null)
		{
			// And we just pressed the interact input
			if (Input.GetButtonDown(m_InteractInput))
				// We start the interaction
				m_CurrentInteractableObject.StartInteract(this);
			
			// If we just released the interact input
			else if (Input.GetButtonUp(m_InteractInput)) 
				// We stop the interaction
				m_CurrentInteractableObject.StopInteract();
		}
	}
}
