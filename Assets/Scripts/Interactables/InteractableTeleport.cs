using UnityEngine;

public class InteractableTeleport : Interactable
{
    [SerializeField]
    Transform teleportDestination;

    [SerializeField]
    GameObject player;

    public override void Interact()
    {
        this.Teleport();
    }

    private void Teleport()
    {
        player.GetComponent<MOVER>().enabled = false;
        player.GetComponent<DemoPlayerController>().enabled = false;
        player.transform.position = teleportDestination.position;
        player.transform.rotation = teleportDestination.rotation;
    }
}
