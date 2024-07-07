using UnityEngine;

/// <summary>
/// Component that lets the character place an object on top of another
/// </summary>
public class PlaceObject : InteractableObject
{
	/// <summary>
	/// Parent in which we place objects
	/// </summary>
	[SerializeField] private Transform m_PlaceParent;
	
	/// <summary>
	/// Reference to the object that is placed here
	/// </summary>
	private GrabObject m_PlacedObject;
	
	public override bool CanInteractWith(PlayerInteraction playerInteraction)
	{
		PlayerGrab playerGrab = playerInteraction.GetComponent<PlayerGrab>();
		
		// We can interact with a Grab Object 
		// if
		// We have a component of type PlayerGrab
		// and
		// We're holding something
		return playerGrab != null && playerGrab.IsGrabbing();
	}
	
	public override void StartInteract(PlayerInteraction playerInteraction)
	{
		// Call base method
		base.StartInteract(playerInteraction);
		
		// Fetch the player grab component
		PlayerGrab playerGrab = playerInteraction.GetComponent<PlayerGrab>();
		// Fetch the object that is grabbed
		GrabObject grabObject = playerGrab.GrabbedObject();
		
		// Release the grabbed object
		playerGrab.Release();
		
		// We set the parent of the object
		grabObject.transform.SetParent(m_PlaceParent);
		// We set its local position to zero
		grabObject.transform.localPosition = Vector3.zero;
		
		// Remember which object was placed here
		m_PlacedObject = grabObject;
		
		// Placing an object is finished instantly. Let's stop the interaction by calling the base method.
		Deselect();
	}
}
