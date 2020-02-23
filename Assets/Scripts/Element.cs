using Knife.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [HideInInspector] public OutlineRegister outline;
    [SerializeField] public bool isTogglable = false;
    [SerializeField] protected Transform routineTransform;
    [HideInInspector] public Vector3 routinePosition;
    [SerializeField] bool isInteractable = true;
    public bool movesCharacter = false;
    public bool toggleMoveValue = true;
    protected bool toggle = false;

    public virtual void Start()
    {
        if(isInteractable)
        {
            outline = GetComponent<OutlineRegister>();
            if (outline == null) outline = GetComponentInChildren<OutlineRegister>();
            outline.enabled = false;
        }
        if(routineTransform)routinePosition = routineTransform.position;
    }

    public void Outline(bool b)
    {
        if (!isInteractable) return;
        outline.enabled = b;
    }

    private void OnMouseOver()
    {
        if (!isInteractable) return;
        Outline(true);
    }

    private void OnMouseExit()
    {
        if (!isInteractable) return;
        Outline(false);
    }

    private void OnMouseDown()
    {
        if (!isInteractable) return;
        Action();
    }

    public virtual void Action()
    {
        if (isTogglable)
        {
            if (toggle) TurnOff();
            else TurnOn();
        }
    }

    public virtual void TurnOn()
    {
        toggle = true;
    }

    public virtual void TurnOff()
    {
        toggle = false;
    }

    public bool isToggle()
    {
        if(toggle==toggleMoveValue) return true;
        return false;
    }

    public virtual void ExecuteRoutine(HumanBehavior human)
    {

    }

    public virtual void QuitRoutine(HumanBehavior human)
    {

    }
}
