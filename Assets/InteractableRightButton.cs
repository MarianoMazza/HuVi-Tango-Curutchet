using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRightButton : Interactable
{
    [SerializeField]
    GameObject avatarList;

    public override void Interact()
    {
        avatarList.GetComponent<SelectAvatar>().right();
    }

    public new void OnPointerExit()
    {
        avatarList.GetComponent<SelectAvatar>().salio = 1;
    }
}
