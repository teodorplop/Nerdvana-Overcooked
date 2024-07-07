using UnityEngine;

/// <summary>
/// Responsible for starting and completing a progress timer when the player interacts with the object
/// </summary>
public class ProgressObject : InteractableObject
{
	/// <summary>
	/// Duration of the progress
	/// </summary>
	[SerializeField] private float m_Duration;
	
	/// <summary>
	/// Current timer
	/// </summary>
	private float m_Timer;
	
	/// <summary>
	/// Whether the progress has finished (timer >= duration)
	/// </summary>
	private bool m_IsProgressFinished;

	public override bool CanInteractWith(PlayerInteraction playerInteraction)
	{
		// We no longer want to be able to interact with this if the progress is finished
		return !m_IsProgressFinished;
	}

	/// <summary>
	/// Returns the progress as a number in [0, 1] interval.
	/// </summary>
	public float GetProgress() 
	{
		return Mathf.Clamp01(m_Timer / m_Duration);
	}
	
	void Update() 
	{
		// If this object is currently being interacted with and the progress isn't yet finished
		if (IsInteracting() && !m_IsProgressFinished) 
		{
			// Count the time
			m_Timer += Time.deltaTime;
			
			Debug.Log($"{name} progress updated: {GetProgress()}");
			
			// We reached the desired duration
			if (m_Timer >= m_Duration)
			{
				// Mark progress as finished
				m_IsProgressFinished = true;
				
				// Deselect the item, it's no longer interactible
				Deselect();
				
				Debug.Log($"{name} progress finished");
			}
		}
	}
}
