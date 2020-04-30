using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonElement : InteractableElement
{
    [SerializeField] DynamicElement buttonTarget;
    AudioSource source;

    public override void Start()
    {
        base.Start();
        source = GetComponent<AudioSource>();
    }
    public override void TurnOn() 
    {
        buttonTarget.TurnOn();
        if(source) source.enabled = true;
        base.TurnOn();
    }

    public override void TurnOff()
    {
        buttonTarget.TurnOff();
        if (source) source.enabled = false;
        base.TurnOff();
    }

    public override void ExecuteRoutine(HumanBehavior human)
    {
        base.ExecuteRoutine(human);
    }

    public override void QuitRoutine(HumanBehavior human)
    {
        base.QuitRoutine(human);
    }
}
