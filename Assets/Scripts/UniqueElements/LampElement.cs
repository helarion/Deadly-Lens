using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampElement : Element
{
    Light l;

    public override void Start()
    {
        base.Start();
        l = GetComponentInChildren<Light>();
    }

    public override void TurnOn()
    {
        base.TurnOn();
        l.enabled = false;
    }

    public override void TurnOff()
    {
        base.TurnOff();
        l.enabled = true;
    }
}
