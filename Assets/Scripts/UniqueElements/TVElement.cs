using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVElement : Element
{
    SpriteRenderer sr;
    Light l;
    AudioSource source;

    public override void Start()
    {
        base.Start();
        sr = GetComponentInChildren<SpriteRenderer>();
        l = GetComponentInChildren<Light>();
        source = GetComponent<AudioSource>();

        toggle = l.enabled;
    }
    public override void TurnOn() 
    {
        sr.enabled = true;
        l.enabled = true;
        source.enabled = true;
        base.TurnOn();
    }

    public override void TurnOff()
    {
        sr.enabled = false;
        l.enabled = false;
        source.enabled = false;
        base.TurnOff();
    }

    public override void ExecuteRoutine(HumanBehavior human)
    {
        base.ExecuteRoutine(human);
        if(!human.hasSat)human.SitDown(routineTransform);
    }

    public override void QuitRoutine(HumanBehavior human)
    {
        base.QuitRoutine(human);
        //if (human.hasSat)
        human.SitUp();
    }
}
