using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopElement : Element
{
    SpriteRenderer sr;
    Light l;
    CameraController cc;
    [SerializeField] Canvas laptopCanvas;

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
        StartCoroutine(WaitCameraAnimation());
    }

    IEnumerator WaitCameraAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        while(cc.inAnimation)
        {
            print("test");
            yield return new WaitForEndOfFrame();
        }
        UiManager.instance.laptopCanvas = laptopCanvas;
        UiManager.instance.EnableCursor();
        UiManager.instance.EnableLaptopCanvas();
        GameManager.instance.UseLaptopElement();
        yield return null;
    }

    public override void ExecuteRoutine(HumanBehavior human)
    {
        base.ExecuteRoutine(human);
        human.IsCautious();
    }

    public override void QuitRoutine(HumanBehavior human)
    {
        base.QuitRoutine(human);
        human.Shrug();
    }
}
