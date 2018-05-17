using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour,IInteractable {
    public bool blocked = false;
    public Animator doorAnimator;

    public void Open()
    {
        if (!blocked)
            doorAnimator.SetTrigger("open");
    }

    public void Close()
    {
        doorAnimator.SetTrigger("close");
    }
    public void Block()
    {
        blocked = true;
        doorAnimator.SetTrigger("block");
    }
    public void Interact()
    {
        Open();
    }
}
