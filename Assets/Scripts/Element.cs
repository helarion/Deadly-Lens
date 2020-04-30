using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AnimStep
{
    public string triggerName;
    public bool needsToWait;
    public float timeToWait;
}

public class Element : MonoBehaviour
{
    public int basicAttentionLevel = 1;
    public Transform routineTransform;
    public Transform interactTransform;
    public List<AnimStep> animStepList;

    //[HideInInspector] public Vector3 routinePosition;
    //[SerializeField] bool isInteractable = true;

    bool launchedAnim = false;

    public virtual void Start()
    {

    }

    public virtual void ExecuteRoutine(HumanBehavior human)
    {

    }

    //IEnumerator AnimationRoutine()
    //{

    //}

    public virtual void QuitRoutine(HumanBehavior human)
    {

    }

    private void SelectAnimation()
    {

    }

    public bool IsUpsetting()
    {
        DynamicElement test = (DynamicElement)this;
        if (test && test.isUpsetting)
            return true;
        return false;
    }
}
