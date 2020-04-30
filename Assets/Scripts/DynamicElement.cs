using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicElement : Element
{
    public bool isTogglable = false;
    public int upsetAttentionLevel = 1;
    protected bool toggle = false;
    public bool isUpsetting = false;

    public override void Start()
    {

    }

    public virtual void Action()
    {
        if (isTogglable)
        {
            if (toggle) TurnOff();
            else TurnOn();

            if (isUpsetting)
            {
                isUpsetting = false;
                GameManager.instance.listUpsetingElements.Remove(this);
            }
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
}
