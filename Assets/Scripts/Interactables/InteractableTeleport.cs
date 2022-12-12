using UnityEngine;

public class InteractableTeleport : Interactable
{
    [SerializeField]
    Transform teleportDestination;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject playerHand;

    [SerializeField]
    AudioSource audioSource;

    public override void Interact()
    {
        this.DropPlayerItem();
        this.Teleport();
    }

    private void Teleport()
    {
        player.GetComponent<MOVER>().enabled = false;
        player.GetComponent<DemoPlayerController>().enabled = false;
        player.transform.GetChild(0).GetComponent<Gaze>().SetAudioSource(audioSource);
        player.transform.position = teleportDestination.position;
        player.transform.rotation = teleportDestination.rotation;
    }

    private void DropPlayerItem()
    {
        if (playerHand.transform.childCount > 0)
        {
            GameObject childGameObject0 = playerHand.transform.GetChild(0).gameObject;
            childGameObject0.transform.parent = null;
            childGameObject0.GetComponent<InteractablePickUp>().ResetRotationAndScale();
            childGameObject0.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
