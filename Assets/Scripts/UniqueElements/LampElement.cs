using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampElement : InteractableElement
{
    Light l;

    public override void Start()
    {
        base.Start();
        l = GetComponentInChildren<Light>();
        toggle = l.enabled;
        //print(name + " is toggled : " + toggle);
    }

    public override void TurnOn()
    {
        base.TurnOn();
        l.enabled = true;
    }

    public override void TurnOff()
    {
        base.TurnOff();
        l.enabled = false;
    }
}
