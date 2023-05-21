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

    [SerializeField]
    AudioClip negativeResponse;

    [SerializeField]
    AudioClip[] positiveResponses;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    bool ejectUpwards;

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
        bool isExpectingFootprint = correctPuzzlePiece[0].CompareTag("Footprint");
        bool receivedExpectedFootprint = isExpectingFootprint && objectCurrentlyHeldByPlayer.CompareTag("Footprint");
        if (objectCurrentlyHeldByPlayer.CompareTag("Distractor") || !ArrayContainsObject(correctPuzzlePiece, objectCurrentlyHeldByPlayer) && !receivedExpectedFootprint)
        {
            StartCoroutine(Eject(objectCurrentlyHeldByPlayer));
            this.NegativeResponse();
        }
        else
        {
            if (objectiveTransformForPuzzlePiece.Length != 0)
            {
                objectiveTransformArrayPosition++;
                if (objectiveTransformForPuzzlePiece.Length == objectiveTransformArrayPosition)
                {
                    this.GetComponent<Collider>().enabled = false;
                }
            }
            else
            {
                this.GetComponent<Collider>().enabled = false;
            }
            puzzleManager.IncreaseObjectiveCount();
            objectCurrentlyHeldByPlayer.GetComponent<InteractablePickUp>().isPositioned = true;
            objectCurrentlyHeldByPlayer.GetComponent<Collider>().enabled = false;
            objectCurrentlyHeldByPlayer.GetComponent<InteractablePickUp>().enabled = false;
            DisablePuzzlePositionMeshRenderersInObjectHierarchy();
            this.PositiveResponse();
        }
    }

    private void DisablePuzzlePositionMeshRenderersInObjectHierarchy()
    {
        if (this.GetComponent<MeshRenderer>() != null)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            Component[] components = this.GetComponentsInChildren(typeof(MeshRenderer), false);
            foreach (MeshRenderer component in components)
            {
                component.enabled = false;
            }
        }
    }

    private void SetHeldObjectToPuzzleposition(GameObject objectCurrentlyHeldByPlayer)
    {
        objectCurrentlyHeldByPlayer.transform.parent = this.transform.parent.transform;
        Vector3 newPosition;
        if (objectiveTransformForPuzzlePiece.Length == 0) {
            objectCurrentlyHeldByPlayer.transform.localRotation = new Quaternion(xRotation, yRotation, 0, 0);
            newPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.12f, this.transform.localPosition.z);
        }
        else
        {
            Transform correctObjectiveTransform = GetCorrectObjectiveTransform(objectiveTransformForPuzzlePiece, objectCurrentlyHeldByPlayer);
            objectCurrentlyHeldByPlayer.transform.localRotation = correctObjectiveTransform.localRotation;
            newPosition = new Vector3(correctObjectiveTransform.localPosition.x, correctObjectiveTransform.localPosition.y, correctObjectiveTransform.localPosition.z);
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
        childGameObject0.GetComponent<Collider>().enabled = true;
        Rigidbody rigidbody = childGameObject0.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddForce((transform.forward * -1) * ejectForce);
        if (ejectUpwards)
        {
            rigidbody.AddForce((transform.up * 1) * ejectForce);
        }
    }

    public void NegativeResponse()
    {
        if (negativeResponse != null)
        {
            audioSource.clip = negativeResponse;
            audioSource.Play();
        }
    }

    public void PositiveResponse()
    {
        AudioClip selectedPositiveResponse = positiveResponses[Random.Range(0, 2)];
        if (selectedPositiveResponse != null)
        {
            audioSource.clip = selectedPositiveResponse;
            audioSource.Play();
        }
    }

    bool ArrayContainsObject(GameObject[] array, GameObject objectCurrentlyHeldByPlayer)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == objectCurrentlyHeldByPlayer)
            {
                return true;
            }
        }
        return false;
    }

    Transform GetCorrectObjectiveTransform(Transform[] array, GameObject objectCurrentlyHeldByPlayer)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].gameObject.name.Equals(objectCurrentlyHeldByPlayer.name))
            {
                return (array[i]);
            }
        }
        return (array[0]);
    }
}
