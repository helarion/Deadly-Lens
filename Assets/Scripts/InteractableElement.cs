using Knife.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableElement : DynamicElement
{
    [HideInInspector] public OutlineRegister outline;

    //public bool movesCharacter = false;
    public bool toggleMoveValue = true;

    public virtual void Start()
    {
        outline = GetComponent<OutlineRegister>();
        if (outline == null) outline = GetComponentInChildren<OutlineRegister>();
        outline.enabled = false;
    }

    public void Outline(bool b)
    {
        //if (!isInteractable) return;
        outline.enabled = b;
    }

    private void OnMouseOver()
    {
        //if (!isInteractable) return;
        Outline(true);
    }

    private void OnMouseExit()
    {
        //if (!isInteractable) return;
        Outline(false);
    }

    private void OnMouseDown()
    {
        //if (!isInteractable) return;
        ActionPlayer();
    }

    public void ActionPlayer()
    {
        Action();
        if (isToggle())
        {
            isUpsetting = true;
            GameManager.instance.listUpsetingElements.Add(this);
        }
    }





    public bool isToggle()
    {
        if (toggle == toggleMoveValue) return true;
        return false;
    }
}
