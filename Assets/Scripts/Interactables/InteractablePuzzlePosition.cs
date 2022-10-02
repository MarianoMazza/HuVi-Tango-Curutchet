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
    GameObject[] correctPuzzlePiece = new GameObject[0];

    [SerializeField]
    float xRotation;

    [SerializeField]
    float yRotation;

    [SerializeField]
    float ejectForce = 300;

    [SerializeField]
    Transform[] objectiveTransformForPuzzlePiece = new Transform[0];

    int timeBeforeEject = 3;
    int objectiveTransformArrayPosition = 0;

    public override void Interact()
    {
        if (playerHand.transform.childCount == 0)
        {
            return;
        }
        GameObject objectCurrentlyHeldByPlayer = playerHand.transform.GetChild(0).gameObject;
        SetHeldObjectToPuzzleposition(objectCurrentlyHeldByPlayer);
        bool isExpectingFootprint = correctPuzzlePiece[objectiveTransformArrayPosition].CompareTag("Footprint");
        bool receivedExpectedFootprint = isExpectingFootprint && objectCurrentlyHeldByPlayer.CompareTag("Footprint");
        if (objectCurrentlyHeldByPlayer.CompareTag("Distractor") || objectCurrentlyHeldByPlayer != correctPuzzlePiece[objectiveTransformArrayPosition] && !receivedExpectedFootprint)
        {
            StartCoroutine(Eject(objectCurrentlyHeldByPlayer));
        }
        else
        {
            if (objectiveTransformForPuzzlePiece.Length != 0)
            {
                objectiveTransformArrayPosition++;
            }
            puzzleManager.IncreaseObjectiveCount();
            objectCurrentlyHeldByPlayer.GetComponent<InteractablePickUp>().isPositioned = true;
            objectCurrentlyHeldByPlayer.GetComponent<InteractablePickUp>().enabled = false;
        }
    }

    private void SetHeldObjectToPuzzleposition(GameObject objectCurrentlyHeldByPlayer)
    {
        objectCurrentlyHeldByPlayer.transform.parent = this.transform.parent.transform;
        objectCurrentlyHeldByPlayer.transform.localRotation = objectiveTransformForPuzzlePiece.Length == 0 ? new Quaternion(xRotation, yRotation, 0, 0) : objectiveTransformForPuzzlePiece[objectiveTransformArrayPosition].localRotation;
        Vector3 newPosition;
        if (objectiveTransformForPuzzlePiece.Length == 0) {
            newPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.12f, this.transform.localPosition.z);
        }
        else
        {
            newPosition = new Vector3(objectiveTransformForPuzzlePiece[objectiveTransformArrayPosition].transform.localPosition.x, objectiveTransformForPuzzlePiece[objectiveTransformArrayPosition].transform.localPosition.y, objectiveTransformForPuzzlePiece[objectiveTransformArrayPosition].transform.localPosition.z);
        }
        objectCurrentlyHeldByPlayer.transform.localPosition = newPosition;
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
        rigidbody.isKinematic = false;
        rigidbody.AddForce((transform.forward * -1) * ejectForce);
    }
}
