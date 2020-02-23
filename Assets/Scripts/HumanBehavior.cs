using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanBehavior : MonoBehaviour
{
    [Header("Routine")]
    [SerializeField] bool doesRoutine = true;
    [SerializeField] List<Element> listRoutine;
    [SerializeField] List<int> timeToSpendInRoutineList;
    int currentRoutineIndex = 0;
    float countRoutineTime = 0;
    bool hasReachedRoutine = false;

    //components
    Animator anim;
    NavMeshAgent agent;

    List<Element> listElements;

    Element objective = null;
    Element lastObjective = null;
    bool hasObjective = false;
    bool isComingBack = false;
    bool isMoving = false;
    bool hasActed = false;
    [HideInInspector]public bool hasSat = false;
    bool isAnimating = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        listElements = new List<Element>();
        anim = GetComponent<Animator>();
        Element[] temp = FindObjectsOfType<Element>();
        foreach(Element e in temp)
        {
            listElements.Add(e);
        }
        //SitDown();
    }

    public void FinishedSit()
    {
        isAnimating = false;
        hasSat = true;
        hasActed = false;
    }

    public void FinishedStand()
    {
        isAnimating = false;
        hasSat = false;
        //isMoving = true;
        //anim.SetBool("IsMoving", true);
        //GoToObjective();
        //hasObjective = true;
    }

    public void IsCautious()
    {
        isAnimating = true;
        anim.SetTrigger("Cautious");
    }

    public void Shrug()
    {
        isAnimating = true;
        anim.SetTrigger("Shrug");
    }

    public void FinishedBlockAnimation()
    {
        isAnimating = false;
    }

    public void FinishedUse()
    {
        isAnimating = false;
        objective.Action();
        LookForObjective();
    }

    void UseElement()
    {
        anim.SetTrigger("Use");
        isAnimating = true;
    }

    public void SitDown(Transform t)
    {
        agent.ResetPath();
        transform.rotation = t.rotation;
        transform.position = t.position;
        isMoving = false;
        isAnimating = true;
        agent.ResetPath();
        isMoving = false;
        isComingBack = false;
        anim.SetTrigger("SitDown");
    }

    public void SitUp()
    {
        isAnimating = true;
        anim.SetTrigger("SitUp");
    }

    void HandlesRoutine()
    {
        if (hasObjective) return;

        if(!hasReachedRoutine)
        {
            agent.SetDestination(listRoutine[currentRoutineIndex].routinePosition);
            if (hasReachedDestination())
                hasReachedRoutine = true;
        }
        else
        {
            listRoutine[currentRoutineIndex].ExecuteRoutine(this);
        }
    }

    bool hasReachedDestination()
    {
        return agent.hasPath && agent.destination != null && agent.remainingDistance <= agent.stoppingDistance;
    }

    void QuitCurrentRoutine()
    {
        hasReachedRoutine = false;
        listRoutine[currentRoutineIndex].QuitRoutine(this);
    }

    void CountRoutine()
    {
        if (!doesRoutine || !hasReachedRoutine) return;
        // Timing handling
        if (listRoutine.Count > 1)
        {
            countRoutineTime += Time.deltaTime;
            if (countRoutineTime >= timeToSpendInRoutineList[currentRoutineIndex])
            {
                QuitCurrentRoutine();
                
                currentRoutineIndex++;
                if (currentRoutineIndex >= listRoutine.Count) currentRoutineIndex = 0;
                countRoutineTime = 0;
                hasReachedRoutine = false;

            }
        }
    }

    void Update()
    {
        CountRoutine();

        // handles the movement animation by calculating its velocity
        if (agent.velocity.x > 1 || agent.velocity.z > 1)
            isMoving = true;
        else
            isMoving = false;
        anim.SetBool("IsMoving", isMoving);

        // stops the AI logic until important animation finishes
        if (isAnimating) return;

        // Objective handling
        LookForObjective(); // is there an objective to work on ?

        if (hasObjective)
        {
            if(hasReachedRoutine)
            {
                QuitCurrentRoutine();
            }
            if (hasReachedDestination())
            {
                if (!hasActed)
                {
                    hasActed = true;
                    UseElement();
                }
            }
        }
        else
        {
            HandlesRoutine();
        }
    }

    void LookForObjective()
    {
        float minDistance =1000;
        Element obj=null;
        foreach (Element e in listElements)
        {
            //if (objective != null) break;
            if (e.movesCharacter && e.isToggle())
            {
                float distance = Vector3.Distance(e.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    obj = e;
                }
            }
        }
        if (obj == null) hasObjective = false;
        if(lastObjective!=obj)
        {
            objective = obj;
            lastObjective = obj;
            if (objective != null) GoToObjective();
        }
    }

    void GoToObjective()
    {
        agent.ResetPath();

        if (hasSat)
            SitUp();
        else
        {
            hasObjective = true;
            agent.SetDestination(objective.transform.position);
            hasActed = false;
            isComingBack = false;
        }
    }
}
