using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraElement : InteractableElement
{
    CameraController controller;

    public override void Start()
    {
        base.Start();
        controller = GetComponentInChildren<CameraController>();
    }

    public override void Action()
    {
        GameManager.instance.SelectCamera(controller);
        base.Action();
    }
}
