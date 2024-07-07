using UnityEngine;

/// <summary>
/// Responsible for spawning and grabbing an object upon interaction
/// </summary>
public class SpawnAndGrabObject : InteractableObject
{
    [SerializeField] private GrabObject objectToSpawn;

    public override bool CanInteractWith(PlayerInteraction playerInteraction)
    {
        PlayerGrab playerGrab = playerInteraction.GetComponent<PlayerGrab>();

        // We can interact
        // If we have a PlayerGrab component
        // and
        // We're not grabbing anything at the moment
        return playerGrab != null && !playerGrab.IsGrabbing();
    }

    public override void StartInteract(PlayerInteraction playerInteraction)
    {
        base.StartInteract(playerInteraction);

        // Spawn the object
        GrabObject spawn = Instantiate(objectToSpawn);

        // Fetch the player grab component
        PlayerGrab playerGrab = playerInteraction.GetComponent<PlayerGrab>();
        // Grab the spawned object
        playerGrab.Grab(spawn);

        // Grabing an object is finished instantly. Let's stop the interaction by calling the base method.
        Deselect();
    }
}
