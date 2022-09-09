using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuzzlePosition : Interactable
{

    [SerializeField]
    GameObject playerHand;

    [SerializeField]
    bool lockPositionedItem;

    [SerializeField]
    PuzzleManager puzzleManager;

    int timeBeforeNextEject = 3;

    public override void Interact()
    {
        if (playerHand.transform.childCount > 0)
        {
            GameObject childGameObject0 = playerHand.transform.GetChild(0).gameObject;
            childGameObject0.transform.parent = this.transform.parent.transform;
            childGameObject0.transform.localRotation = new Quaternion(0, 0, 0, 0);
            Vector3 newPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.12f, this.transform.localPosition.z);
            childGameObject0.transform.localPosition = newPosition;
            if (childGameObject0.CompareTag("Distractor"))
            {
                StartCoroutine(Eject(childGameObject0));
                return;
            }
            puzzleManager.IncreaseObjectiveCount();
            childGameObject0.GetComponent<InteractablePickUp>().isPositioned = true;
            if (lockPositionedItem)
            {
                childGameObject0.GetComponent<InteractablePickUp>().enabled = false;
            }
        }
    }

    public void LoseObjective()
    {
        puzzleManager.DecreaseObjectiveCount();
    }

    private IEnumerator Eject(GameObject childGameObject0)
    {
        yield return new WaitForSeconds(timeBeforeNextEject);
        childGameObject0.transform.parent = null;
        childGameObject0.GetComponent<InteractablePickUp>().ResetRotationAndScale();
        childGameObject0.GetComponent<Rigidbody>().isKinematic = false;
    }

}
