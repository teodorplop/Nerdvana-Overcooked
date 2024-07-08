using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Responsible for making a <see cref="NavMeshAgent"/> follow a target
/// </summary>
public class FollowTarget : MonoBehaviour
{
    /// <summary>
    /// Which target should this object follow
    /// </summary>
    [SerializeField] private Transform m_TargetToFollow;

    /// <summary>
    /// Reference to the <see cref="NavMeshAgent"/>
    /// </summary>
    private NavMeshAgent m_NavAgent;

    void Awake()
    {
        // Fetch the component for navigation
        m_NavAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (m_TargetToFollow != null)
        {
            // Set the destination
            m_NavAgent.SetDestination(m_TargetToFollow.position);
        }
    }
}
