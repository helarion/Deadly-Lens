using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanBehavior : MonoBehaviour
{
    Animator anim;
    List<Element> listElements;

    NavMeshAgent agent;

    bool hasObjective = false;
    bool isComingBack = false;
    Element objective = null;

    Vector3 SpawnPosition;
    Quaternion SpawnRotation;

    [SerializeField] float lerpTimeMove = 1;
    [SerializeField] float lerpTimeRotate = 2;

    bool isMoving = false;
    bool isLerping = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPosition = transform.position;
        SpawnRotation = transform.rotation;
        agent = GetComponent<NavMeshAgent>();
        listElements = new List<Element>();
        anim = GetComponent<Animator>();
        Element[] temp = FindObjectsOfType<Element>();
        foreach(Element e in temp)
        {
            listElements.Add(e);
        }
    }

    private IEnumerator SitUp()
    {
        anim.SetTrigger("SitUp");
        int i = 0;
        while (i<1)
        {
            yield return new WaitForSeconds(1);
            i++;
        }
        agent.SetDestination(objective.transform.position);
        anim.SetBool("IsSat", false);
        anim.SetBool("IsMoving", true);
        hasObjective = true;
    }

    private IEnumerator SitDown()
    {
        isMoving = true;
        anim.SetBool("IsMoving", false);
        isLerping = true;
        int i = 0;
        while (i < 1)
        {
            yield return new WaitForSeconds(1);
            i++;
        }
        isLerping = false;
        anim.SetTrigger("SitDown");
        while (i < 2)
        {
            yield return new WaitForSeconds(1);
            i++;
        }
        objective = null;
        hasObjective = false;
        isComingBack = false;
        anim.SetBool("IsSat", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isLerping)
        {
            transform.position = Vector3.Lerp(transform.position, SpawnPosition, Time.deltaTime/lerpTimeMove);
            transform.rotation = Quaternion.Lerp(transform.rotation, SpawnRotation, Time.deltaTime/lerpTimeRotate);
        }
        if(hasObjective)
        {    
            if(agent.destination!=null && agent.remainingDistance <= agent.stoppingDistance)
            {
                objective.TurnOff();
                if (!isComingBack)
                {
                    agent.SetDestination(SpawnPosition);
                    isComingBack = true;
                }
                else
                {
                    if (!isMoving)
                    {
                        StartCoroutine("SitDown");
                        isMoving = true;
                    }
                }
            }
        }
        else
        {
            Element e =LookForObjective();
            if (e != null) GoToObjective(e);
        }
    }

    Element LookForObjective()
    {
        foreach (Element e in listElements)
        {
            if (objective != null) break;
            if (e.movesCharacter && e.isToggle())
            {
                return e;
            }
        }
        return null;
    }

    void GoToObjective(Element e)
    {
        isMoving = false;
        objective = e;
        StartCoroutine("SitUp");
    }
}
