using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableOkButton : Interactable
{
    [SerializeField]
    GameObject avatarList;

    public override void Interact()
    {
        avatarList.GetComponent<SelectAvatar>().selectedAvatar();
    }
}
