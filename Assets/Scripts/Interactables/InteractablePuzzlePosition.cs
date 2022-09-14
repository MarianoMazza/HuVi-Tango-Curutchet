using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuzzlePosition : Interactable
{
    [SerializeField]
    GameObject playerHand;

    [SerializeField]
    PuzzleManager puzzleManager;

    [SerializeField]
    GameObject correctPuzzlePiece;

    [SerializeField]
    float xRotation;

    [SerializeField]
    float yRotation;

    [SerializeField]
    Transform objectiveTransformForPuzzlePiece;

    int timeBeforeEject = 3;

    public override void Interact()
    {
        if (playerHand.transform.childCount == 0)
        {
            return;
        }
        GameObject childGameObject0 = playerHand.transform.GetChild(0).gameObject;
        childGameObject0.transform.parent = this.transform.parent.transform;
        childGameObject0.transform.localRotation = objectiveTransformForPuzzlePiece == null ? new Quaternion(xRotation, yRotation, 0, 0) : objectiveTransformForPuzzlePiece.localRotation;
        Vector3 newPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.12f, this.transform.localPosition.z);
        childGameObject0.transform.localPosition = newPosition;
        bool isExpectingFootprint = correctPuzzlePiece.CompareTag("Footprint");
        bool receivedExpectedFootprint = isExpectingFootprint && childGameObject0.CompareTag("Footprint");
        if (childGameObject0.CompareTag("Distractor") || childGameObject0 != correctPuzzlePiece && !receivedExpectedFootprint)
        {
            StartCoroutine(Eject(childGameObject0));
        }
        else
        {
            puzzleManager.IncreaseObjectiveCount();
            childGameObject0.GetComponent<InteractablePickUp>().isPositioned = true;
            childGameObject0.GetComponent<InteractablePickUp>().enabled = false;
        }
    }

    public void LoseObjective()
    {
        puzzleManager.DecreaseObjectiveCount();
    }

    private IEnumerator Eject(GameObject childGameObject0)
    {
        yield return new WaitForSeconds(timeBeforeEject);
        childGameObject0.transform.parent = null;
        childGameObject0.GetComponent<InteractablePickUp>().ResetRotationAndScale();
        Rigidbody rigidbody = childGameObject0.GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * 300);
        childGameObject0.GetComponent<Rigidbody>().isKinematic = false;
    }

}
