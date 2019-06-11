using Knife.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [HideInInspector] public OutlineRegister outline;
    [SerializeField] public bool isTogglable = false;
    public bool movesCharacter = false;
    bool toggle = false;

    public virtual void Start()
    {
        outline = GetComponent<OutlineRegister>();
        outline.enabled = false;
    }

    public void Outline(bool b)
    {
        outline.enabled = b;
    }

    private void OnMouseOver()
    {
        Outline(true);
    }

    private void OnMouseExit()
    {
        Outline(false);
    }

    private void OnMouseDown()
    {
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
        return toggle;
    }
}
