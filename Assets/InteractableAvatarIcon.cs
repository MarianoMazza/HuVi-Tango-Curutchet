using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAvatarIcon : Interactable
{
    [SerializeField]
    GameObject avatarManager;

    public override void Interact()
    {
        avatarManager.GetComponent<SelectAvatarMnager>().Avatar();
    }
}
