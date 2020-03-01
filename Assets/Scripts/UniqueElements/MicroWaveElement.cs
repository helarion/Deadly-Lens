using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroWaveElement : Element
{
    Light l;
    AudioSource source;

    public override void Start()
    {
        base.Start();
        l = GetComponentInChildren<Light>();
        source = GetComponent<AudioSource>();
    }

    public override void TurnOn()
    {
        base.TurnOn();
        l.enabled = true;
        source.enabled = true;
    }

    public override void TurnOff()
    {
        base.TurnOff();
        l.enabled = false;
        source.enabled = false;
    }

    public override void ExecuteRoutine(HumanBehavior human)
    {
        base.ExecuteRoutine(human);
        human.DefaultAct();
    }

    public override void QuitRoutine(HumanBehavior human)
    {
        base.QuitRoutine(human);
        human.DefaultAct();
    }
}
