using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(100)]
public class HumanBehavior : MonoBehaviour
{
    #region variables
    [Header("Animation events")]
    public static readonly int hashSitDown = Animator.StringToHash("SitDown");
    public static readonly int hashSitUp = Animator.StringToHash("SitUp");
    public static readonly int hashMove = Animator.StringToHash("IsMoving");
    public static readonly int hashDefaultAct = Animator.StringToHash("DefaultAct");
    public static readonly int hashShrug = Animator.StringToHash("Shrug");

    [Header("Compnents")]
    Animator anim;
    NavMeshAgent agent;

    [Header("Routine")]
    [SerializeField] List<Element> listRoutine;
    [SerializeField] List<int> timeToSpendInRoutineList;
    bool routineHasActed = false;
    bool routineHasQuit = false;
    bool upsetHasActed = false;
    bool hasShrugged = false;
    bool hasFixed = false;

    int currentRoutineIndex = 0;
    float countRoutineTime = 0;
    int timerToReachRoutine = 0;

    [Header("AI state")]
    private AIState currentAIState=AIState.RoutineGoTo;
    private enum AIState
    {
        RoutineGoTo,
        RoutineAct,
        RoutineWait,
        UpsetAct,
        UpsetGoto,
        UpsetWait
    }
    public int currentAttentionLevel = 0;
    bool upsetWaitObjective = false;

    [Header("Current Objective")]
    Transform target;
    Element currentObjective;
    Element currentUpset;
    #endregion

    #region Start Update

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        currentObjective = listRoutine[currentRoutineIndex];
        target = currentObjective.routineTransform;
        currentAttentionLevel = currentObjective.basicAttentionLevel;
        SceneLinkedSMB<HumanBehavior>.Initialise(anim, this);
    }

    void Update()
    {
        HandlingUpsettingElements();
        switch(currentAIState)
        {
            case AIState.RoutineGoTo:
                if(!hasReachedDestination())
                {
                    routineHasActed = false;
                    routineHasQuit = false;
                    MoveTo(target.position);
                }
                else
                {
                    StopMove();
                    currentAIState = AIState.RoutineAct;
                }
                break;
            case AIState.RoutineAct:
                if(currentObjective.isUpsetting)
                {
                    currentUpset = currentObjective;
                    currentAIState = AIState.UpsetGoto;
                }
                else if(!routineHasActed)
                {
                    routineHasActed = true;
                    currentObjective.ExecuteRoutine(this);
                }
                break;
            case AIState.RoutineWait:
                if (listRoutine.Count > 1 && !routineHasQuit)
                {
                    countRoutineTime += Time.deltaTime;
                    if (countRoutineTime >= timeToSpendInRoutineList[currentRoutineIndex])
                    {
                        routineHasQuit = true;
                        currentObjective.QuitRoutine(this);
                    }
                    else
                    {
                        UpsetWaitHandling();
                    }
                }
                break;
            case AIState.UpsetGoto:
                if (!hasReachedDestination())
                {
                    hasShrugged = false;
                    upsetHasActed = false;
                    MoveTo(target.position);
                }
                else
                {
                    StopMove();
                    currentAIState = AIState.UpsetWait;
                }
                break;
            case AIState.UpsetAct:
                if (!upsetHasActed)
                {
                    upsetHasActed = true;
                    DefaultAct();
                }
                break;
            case AIState.UpsetWait:
                if(!hasShrugged)
                {
                    hasShrugged = true;
                    Shrug();
                }
                break;
        }
    }
    #endregion

    void HandlingUpsettingElements()
    {
        if (GameManager.instance.listUpsetingElements.Count <= 0) return;

        int maxAttention = 0;
        float minDistance = 1000;
        Element obj = null;
        foreach (Element e in GameManager.instance.listUpsetingElements)
        {
            if(e.upsetAttentionLevel > currentAttentionLevel)
            {
                float distance = Vector3.Distance(e.transform.position, transform.position);
                if(e.upsetAttentionLevel > maxAttention)
                {
                    maxAttention = e.upsetAttentionLevel;
                }
                if (distance < minDistance)
                {
                    minDistance = distance;
                    if(e.upsetAttentionLevel >= maxAttention) obj = e;
                }
            }
            if(obj && obj!=currentUpset)
            {
                upsetWaitObjective = true;
                currentUpset = obj;
                currentAttentionLevel = currentUpset.upsetAttentionLevel;
            }
        }
    }

    void ReturnToRoutine()
    {
        currentUpset = null;
        target = currentObjective.routineTransform;
        currentAIState = AIState.RoutineGoTo;
        currentAttentionLevel = currentObjective.basicAttentionLevel;
    }

    void UpsetWaitHandling()
    {
        if(upsetWaitObjective && currentUpset && currentAIState!=AIState.UpsetGoto)
        {
            upsetWaitObjective = false;
            target = currentUpset.routineTransform;
            currentAIState = AIState.UpsetGoto;
        }
    }

    bool HasQuit()
    {
        return routineHasQuit;
    }

    #region Movement
    void MoveTo(Vector3 position)
    {
        anim.SetBool(hashMove, true);
        agent.SetDestination(position);
        UpsetWaitHandling();
    }

    bool hasReachedDestination()
    {
        return agent.hasPath && agent.destination != null && agent.remainingDistance <= agent.stoppingDistance;
    }

    void StopMove()
    {
        agent.ResetPath();
        anim.SetBool(hashMove, false);
    }
    #endregion

    #region Animation Triggers
    public void DefaultAct()
    {
        anim.SetTrigger(hashDefaultAct);
    }

    public void SitDown()
    {
        transform.LookAt(currentObjective.transform.position);
        anim.SetTrigger(hashSitDown);
    }

    public void SitUp()
    {
        anim.SetTrigger(hashSitUp);
    }

    public void Shrug()
    {
        anim.SetTrigger(hashShrug);
    }
    #endregion

    #region animation events
    public void InteractObjective()
    {
        if(currentAIState==AIState.RoutineAct || currentAIState == AIState.RoutineWait)
        {
            currentObjective.Action();
        }
        else if(currentAIState == AIState.UpsetAct)
        {
            currentUpset.Action();
        }
    }

    public void WaitRoutine()
    {
        currentAIState = AIState.RoutineWait;
        UpsetWaitHandling();
    }

    public void NextRoutine()
    {
        currentRoutineIndex++;
        if (currentRoutineIndex >= listRoutine.Count) currentRoutineIndex = 0;
        countRoutineTime = 0;
        target = listRoutine[currentRoutineIndex].routineTransform;
        currentObjective = listRoutine[currentRoutineIndex];
        currentAIState = AIState.RoutineGoTo;
        UpsetWaitHandling();
    }

    public void ShrugHandling()
    {
        currentAIState = AIState.UpsetAct;
    }

    public void ObjectAct()
    {
        if(currentAIState == AIState.RoutineAct || currentAIState == AIState.RoutineWait)
        {
            if (!HasQuit())
                WaitRoutine();
            else
                NextRoutine();
        }
        else if(currentAIState == AIState.UpsetAct)
        {
            ReturnToRoutine();
        }
    }
    #endregion
}
