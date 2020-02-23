using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] int xMax=60;
    [SerializeField] int xMin = -30;
    [SerializeField] int yMax=25;
    [SerializeField] int yMin = -25;
    [SerializeField] float speedRotation=50;
    [SerializeField] int maxZoom = 70;
    [SerializeField] int minZoom = 15;
    [SerializeField] float zoomModifier = 1f;
    [SerializeField] public Transform camTransform;
    [SerializeField] public bool isControlled = false;

    public Camera cam;

    private float xPitch = 0f;
    private float yPitch = 0f;

    public float xStart;
    public float yStart;

    float savedXStart;
    float savedYStart;

    public bool inAnimation = false;

    float fovGoal;
    int fovModifier = 70;
    float fovSpeed = 0.1f;
    int fovRange = 5;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isControlled || GameManager.instance.isOnLaptop) return;
        CameraRotation();

        xStart = transform.parent.eulerAngles.x+savedXStart;
        yStart = transform.parent.eulerAngles.y+savedYStart;

        //print("xstart:" + xStart);
    }

    private void LateUpdate()
    {
        if (!isControlled || GameManager.instance.isOnLaptop) return;
        Zoom();
    }

    public void Init()
    {
        xStart += camTransform.localEulerAngles.x;
        yStart += camTransform.localEulerAngles.y;
        savedXStart = xStart;
        savedYStart = yStart;
    }

    public void CameraRotation()
    {
        xPitch -= Input.GetAxis("Mouse Y") * speedRotation;
        if(yMin!=-1 && yMax!=-1) xPitch = Mathf.Clamp(xPitch, yMin, yMax);
        yPitch -= Input.GetAxis("Mouse X")*-1 * speedRotation;
        if(xMin != -1 && xMax != -1) yPitch = Mathf.Clamp(yPitch, xMin, xMax);
        camTransform.eulerAngles = new Vector3(xPitch+xStart, yPitch+yStart, 0f);
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (cam.fieldOfView > minZoom) cam.fieldOfView -= zoomModifier;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (cam.fieldOfView < maxZoom) cam.fieldOfView += zoomModifier;
        }
    }

    public void ResetZoom()
    {
        cam.fieldOfView = maxZoom;
    }

    public virtual void StartControl()
    {
        fovGoal = cam.fieldOfView + fovModifier;
        StartCoroutine(WaitCameraAnimation(true));
    }

    public virtual void StopControl()
    {
        //print("current fov:" + cam.fieldOfView);
        fovGoal = cam.fieldOfView + fovModifier;
        StartCoroutine(WaitCameraAnimation(false));
    }

    IEnumerator WaitCameraAnimation(bool control)
    {
        inAnimation = true;
        while(fovGoal - cam.fieldOfView > fovRange)
        {
            //print(fovGoal);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fovGoal, fovSpeed);
            yield return new WaitForEndOfFrame();
        }

        inAnimation = false;
        cam.enabled = control;
        isControlled = control;
        fovGoal = cam.fieldOfView - fovModifier;

        while (cam.fieldOfView - fovGoal > fovRange)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fovGoal, fovSpeed);
            yield return new WaitForEndOfFrame();
        }
        if (!isControlled) ResetZoom();
    }
}
