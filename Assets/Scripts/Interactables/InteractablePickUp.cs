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
            //childGameObject0.transform.parent = PetroglyphTable.transform; here the object being held would stop being the hand's child
            //childGameObject0.transform.localPosition = startingPosition;
            childGameObject0.transform.localRotation = startingRotation;
            childGameObject0.transform.localScale = startingScale;
            this.transform.parent = playerHand.transform;
            this.transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            transform.parent = playerHand.transform;
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}