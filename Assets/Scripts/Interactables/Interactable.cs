﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Interactable() { }

    public abstract void Interact();

    public void OnPointerExit(){}
}
