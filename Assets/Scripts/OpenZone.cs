using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenZone : MonoBehaviour
{
    bool isOpened = false;
    Transform drawer;
    [SerializeField] float move = 4;

    Vector3 newPos;

    private void Start()
    {
        drawer = transform.parent;
        newPos = drawer.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        RobotController robot = other.GetComponent<RobotController>();
        if (robot == null || robot.activeObject != null) return;
        UiManager.instance.GrabTextEnabled(true);
    }

    private void OnTriggerStay(Collider other)
    {
        RobotController robot = other.GetComponent<RobotController>();
        if (robot == null || robot.activeObject != null) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RobotController robot = other.GetComponent<RobotController>();
        if (robot == null || robot.activeObject != null) return;
        UiManager.instance.GrabTextEnabled(false);
    }

    void Interact()
    {
        if(isOpened)
        {
            newPos.z -=move;
            isOpened = false;
        }
        else
        {
            newPos.z +=move;
            isOpened = true;
        }
    }

    private void Update()
    {
        drawer.localPosition = Vector3.Lerp(drawer.localPosition, newPos, Time.deltaTime);
    }
}
