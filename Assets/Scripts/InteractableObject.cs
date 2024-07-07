using UnityEngine;

/// <summary>
/// Responsible for handling objects that players can interact with
/// </summary>
public class InteractableObject : MonoBehaviour
{
	private PlayerInteraction m_PlayerInteraction;
	
	/// <summary>
	/// Selects the object. Most likely, it's going to enable a highlight on it.
	/// </summary>
	public void Select()
	{
		Debug.Log($"Object highlighted for interaction: {name}", this);
	}
	
	/// <summary>
	/// Deselects the object
	/// </summary>
	public void Deselect() 
	{
		// Make sure we also stop interacting if we're deselecting the object
		StopInteract();
		
		Debug.Log($"Stopped highlighting object for interaction: {name}", this);
	}
	
	/// <summary>
	/// Whether player can interact with this object. <br/>
	/// Virtual method, can be overriden.
	/// </summary>
	public virtual bool CanInteractWith(PlayerInteraction playerInteraction) 
	{
		// By default, let's assume that all objects can be interacted with
		return true;
	}
	
	/// <summary>
	/// Starts interaction with the object. <br/>
	/// Virtual method, can be overriden.
	/// </summary>
	public virtual void StartInteract(PlayerInteraction playerInteraction) 
	{
		// If this object is already being interacted with, ignore this attempt of interaction
		if (m_PlayerInteraction != null) 
		{
			Debug.LogWarning($"{m_PlayerInteraction} is already interacting with object {name}", this);
			return;
		}
		
		// Remember who interacts with this object
		m_PlayerInteraction = playerInteraction;
		Debug.Log($"{playerInteraction} started interaction with object: {name}", this);
	}
	
	/// <summary>
	/// Stops interaction with the object. <br/>
	/// Virtual method, can be overriden.
	/// </summary>
	public virtual void StopInteract() 
	{
		// If this object is currently being interacted with
		if (m_PlayerInteraction != null) 
		{
			Debug.Log($"{m_PlayerInteraction} stopped interaction with object: {name}", this);
			
			// Stop the interaction
			m_PlayerInteraction = null;
		}
	}
}
