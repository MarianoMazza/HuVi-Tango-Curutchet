using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLeftButton : Interactable
{
    [SerializeField]
    GameObject avatarList;

    public override void Interact()
    {
        avatarList.GetComponent<SelectAvatar>().left();
    }

    public new void OnPointerExit()
    {
        avatarList.GetComponent<SelectAvatar>().salio = 1;
    }
}
