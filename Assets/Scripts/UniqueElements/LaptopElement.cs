using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopElement : Element
{
    SpriteRenderer sr;
    Light l;
    CameraController cc;

    public override void Start()
    {
        base.Start();
        sr = GetComponentInChildren<SpriteRenderer>();
        l = GetComponentInChildren<Light>();
        cc = GetComponentInChildren<CameraController>();
    }
    public override void Action()
    {
        sr.enabled = false;
        l.enabled = true;        
        GameManager.instance.SelectCamera(cc);
    }
}
