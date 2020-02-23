using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabZone : MonoBehaviour
{
    GrabObject obj;

    private void Start()
    {
        obj = GetComponentInParent<GrabObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Robot") return;
        RobotController robot = other.transform.parent.GetComponent<RobotController>();
        if (robot == null || !robot.isControlled || robot.activeObject !=null) return;
        UiManager.instance.GrabTextEnabled(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Robot") return;
        RobotController robot = other.transform.parent.GetComponent<RobotController>();
        if (robot == null || !robot.isControlled || robot.activeObject != null) return;
        if (Input.GetAxisRaw("Use") != 0)
        {
            robot.Grab(obj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Robot") return;
        RobotController robot = other.transform.parent.GetComponent<RobotController>();
        if (robot == null || !robot.isControlled || robot.activeObject != null) return;
        UiManager.instance.GrabTextEnabled(false);
    }
}
