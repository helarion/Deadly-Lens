using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorElement : Element
{
    [SerializeField] float doorSize;
    [SerializeField] float openSpeed = 0.5f;
    [SerializeField] float closeSpeed = 0.2f;
    float initialSize;
    bool closing = false;
    bool opening = false;
    AudioSource source;

    public override void Start()
    {
        base.Start();
        initialSize = transform.localScale.y;
        source = GetComponent<AudioSource>();
    }
    public override void TurnOn() 
    {
        opening = true;
        StartCoroutine(OpenDoor());
        if(source)source.enabled = true;
        base.TurnOn();
    }

    public override void TurnOff()
    {
        closing = true;
        StartCoroutine(CloseDoor());
        if(source)source.enabled = false;
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

    IEnumerator OpenDoor()
    {
        closing = false;
        Vector3 newScale = transform.localScale;
        while(opening && transform.localScale.y<initialSize+doorSize)
        {
            newScale.y = Mathf.Lerp(newScale.y, initialSize + doorSize, Time.deltaTime * openSpeed);
            transform.localScale = newScale;
            yield return new WaitForEndOfFrame();
        }
        opening = false;
        yield return null;
    }

    IEnumerator CloseDoor()
    {
        opening = false;
        Vector3 newScale = transform.localScale;
        while (closing && transform.localScale.y > initialSize)
        {
            newScale.y = Mathf.Lerp(newScale.y, initialSize, Time.deltaTime * closeSpeed);
            transform.localScale = newScale;
            yield return new WaitForEndOfFrame();
        }
        closing = false;
        yield return null;
    }
}
