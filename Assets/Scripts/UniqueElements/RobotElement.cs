using Knife.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotElement : InteractableElement
{
    RobotController controller;

    public override void Start()
    {
        outline = GetComponentInChildren<OutlineRegister>();
        if (outline != null) outline.enabled = false;
        controller = GetComponent<RobotController>();
    }

    public override void Action()
    {
        GameManager.instance.SelectCamera(controller);
        base.Action();
    }
}
