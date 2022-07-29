using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuzzlePosition : Interactable
{

    [SerializeField]
    GameObject playerHand;

    public override void Interact()
    {
        if (playerHand.transform.childCount > 0)
        {
            GameObject childGameObject0 = playerHand.transform.GetChild(0).gameObject;
            childGameObject0.transform.parent = this.transform.parent.transform;
            childGameObject0.transform.localRotation = new Quaternion(0, 0, 0, 0);
            childGameObject0.transform.localScale = this.transform.localScale;
            Vector3 newPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.12f, this.transform.localPosition.z);
            childGameObject0.transform.localPosition = newPosition;
        }
    }
}
