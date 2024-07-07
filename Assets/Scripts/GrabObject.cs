/// <summary>
/// Component that lets the character grab the object
/// </summary>
public class GrabObject : InteractableObject
{
	public override bool CanInteractWith(PlayerInteraction playerInteraction)
	{
		PlayerGrab playerGrab = playerInteraction.GetComponent<PlayerGrab>();
		
		// We can interact with a Grab Object 
		// if
		// We have a component of type PlayerGrab
		// and
		// We're not already grabbing something
		return playerGrab != null && !playerGrab.IsGrabbing();
	}

	public override void StartInteract(PlayerInteraction playerInteraction)
	{
		// Call base method
		base.StartInteract(playerInteraction);
		
		// Fetch the player grab component
		PlayerGrab playerGrab = playerInteraction.GetComponent<PlayerGrab>();
		// Grab this object
		playerGrab.Grab(this);
		
		// Grabing an object is finished instantly. Let's stop the interaction by calling the base method.
		Deselect();
	}
}
