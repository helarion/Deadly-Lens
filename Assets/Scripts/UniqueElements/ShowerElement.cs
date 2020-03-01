using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerElement : Element
{
    ParticleSystem water;
    AudioSource source;

    public override void Start()
    {
        base.Start();
        water = GetComponentInChildren<ParticleSystem>();
        source = GetComponent<AudioSource>();
    }

    public override void TurnOn()
    {
        base.TurnOn();
        source.enabled = true;
        water.gameObject.SetActive(true);
        water.Play();
    }

    public override void TurnOff()
    {
        base.TurnOff();
        source.enabled = false;
        water.gameObject.SetActive(false);
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
