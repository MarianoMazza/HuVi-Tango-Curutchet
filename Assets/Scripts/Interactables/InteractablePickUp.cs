using UnityEngine;

public class InteractablePickUp : Interactable
{
    [SerializeField]
    GameObject playerHand;

    Vector3 startingPosition;
    Quaternion startingRotation;
    Vector3 startingScale;

    private void Start()
    {
        startingPosition = this.transform.localPosition;
        startingScale = this.transform.localScale;
        startingRotation = this.transform.localRotation;
    }

    public override void Interact()
    {
        if (playerHand.transform.childCount > 0)
        {
            GameObject childGameObject0 = playerHand.transform.GetChild(0).gameObject;
            childGameObject0.transform.parent = null;
            childGameObject0.GetComponent<InteractablePickUp>().ResetRotationAndScale();
            childGameObject0.GetComponent<Rigidbody>().isKinematic = false;
            this.transform.parent = playerHand.transform;
            this.transform.localPosition = new Vector3(0, 0, 0);
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            this.transform.parent = playerHand.transform;
            this.transform.localPosition = new Vector3(0, 0, 0);
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void ResetRotationAndScale()
    {
        this.transform.localRotation = startingRotation;
        this.transform.localScale = startingScale;
    }
}