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
        RobotController robot = other.GetComponent<RobotController>();
        if (robot == null || robot.activeObject !=null) return;
        UiManager.instance.GrabTextEnabled(true);
    }

    private void OnTriggerStay(Collider other)
    {
        RobotController robot = other.GetComponent<RobotController>();
        if (robot == null || robot.activeObject != null) return;
        if (Input.GetAxisRaw("Use") != 0)
        {
            robot.Grab(obj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RobotController robot = other.GetComponent<RobotController>();
        if (robot == null || robot.activeObject != null) return;
        UiManager.instance.GrabTextEnabled(false);
    }
}
