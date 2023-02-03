using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : Actor
{
    public override void OnConfirm()
    {
        GameObject firewood = GameObject.Find("Firewood");
        Interactable interactable = firewood.GetComponent<Interactable>();
        interactable.Interact();
    }
}
