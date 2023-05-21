using UnityEngine;

public class InteractablePickUp : Interactable
{
    [SerializeField]
    GameObject playerHand;

    [SerializeField]
    AudioClip soundToBePlayed;

    [SerializeField]
    AudioSource tangoRoomGuide;

    Quaternion startingRotation;
    Vector3 startingScale;
    public bool isPositioned { get; set; }

    private void Start()
    {
        startingScale = this.transform.localScale;
        startingRotation = this.transform.localRotation;
    }

    public override void Interact()
    {
        PlaySound();
        if (isPositioned)
        {
            this.transform.parent.GetChild(0).GetComponent<InteractablePuzzlePosition>().LoseObjective();
            isPositioned = false;
        }
        if (playerHand.transform.childCount > 0)
        {
            GameObject childGameObject0 = playerHand.transform.GetChild(0).gameObject;
            childGameObject0.transform.parent = null;
            childGameObject0.GetComponent<Collider>().enabled = true;
            childGameObject0.GetComponent<InteractablePickUp>().ResetRotationAndScale();
            childGameObject0.GetComponent<Rigidbody>().isKinematic = false;
            this.transform.parent = playerHand.transform;
            this.transform.localPosition = new Vector3(0, 0, 0);
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            this.transform.parent = playerHand.transform;
            this.transform.localPosition = new Vector3(0, 0, 0);
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void ResetRotationAndScale()
    {
        this.transform.localRotation = startingRotation;
        this.transform.localScale = startingScale;
    }

    public void PlaySound()
    {
        if (soundToBePlayed != null)
        {
            AudioSource audioSource;
            if (tangoRoomGuide != null)
            {
                audioSource = tangoRoomGuide;
            } 
            else
            {
                audioSource = this.GetComponent<AudioSource>();
            }
            audioSource.clip = soundToBePlayed;
            audioSource.Play();
        }
    }
}