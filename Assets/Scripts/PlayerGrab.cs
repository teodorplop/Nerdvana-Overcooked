using UnityEngine;

/// <summary>
/// Responsible for grabbing and releasing objects
/// </summary>
public class PlayerGrab : MonoBehaviour
{
	/// <summary>
	/// Parent transform which will hold the grabbed object
	/// </summary>
	[SerializeField] private Transform m_GrabParent;
	
	/// <summary>
	/// Input for releasing, configured in Edit -> Project Settings -> Input Manager
	/// </summary>
	[SerializeField] private string m_ReleaseInput;
	
	/// <summary>
	/// Object that is currently being grabbed
	/// </summary>
	private GrabObject m_GrabbedObject;
	
	/// <summary>
	/// Whether we're already grabbing an object
	/// </summary>
	public bool IsGrabbing() 
	{
		return m_GrabbedObject != null;
	}
	
	/// <summary>
	/// Returns the object that is currently grabbed
	/// </summary>
	public GrabObject GrabbedObject() 
	{
		return m_GrabbedObject;
	}
	
	/// <summary>
	/// Grabs the object
	/// </summary>
	public void Grab(GrabObject objectToGrab)
	{
		// Disable collision of the object we're about to grab
		objectToGrab.GetComponent<Collider>().enabled = false;
		
		// We set the parent of the object
		objectToGrab.transform.SetParent(m_GrabParent);
		// We set its local position to zero
		objectToGrab.transform.localPosition = Vector3.zero;
		
		// We remember the object we grabbed
		m_GrabbedObject = objectToGrab;
	}
	
	/// <summary>
	/// Releases the grabbed object
	/// </summary>
	public void Release() 
	{
		// If we are holding any object
		if (m_GrabbedObject != null) 
		{
			// We are removing its parent
			m_GrabbedObject.transform.SetParent(null, true);
			
			// Restore collision of the object upon releasing
			m_GrabbedObject.GetComponent<Collider>().enabled = true;
			
			// We are no longer holding anything
			m_GrabbedObject = null;
		}
	}
	
	void Update() 
	{
		if (Input.GetButtonDown(m_ReleaseInput)) 
		{
			// Release the object, if we pressed the release input
			Release();
		}
	}
}
