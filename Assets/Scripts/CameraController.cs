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
    [SerializeField] float modifier = 1f;
    [SerializeField] public Transform camTransform;
    [SerializeField] public bool isControlled = false;

    public Camera cam;

    private float xPitch = 0f;
    private float yPitch = 0f;

    public float xStart;
    public float yStart;

    float savedXStart;
    float savedYStart;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isControlled) return;
        CameraCheck();
        
        xStart = transform.parent.eulerAngles.x+savedXStart;
        yStart = transform.parent.eulerAngles.y+savedYStart;

        print("xstart:" + xStart);
    }

    public void Init()
    {
        xStart += camTransform.localEulerAngles.x;
        yStart += camTransform.localEulerAngles.y;
        savedXStart = xStart;
        savedYStart = yStart;
    }

    public void CameraCheck()
    {
        RotationConstrainMovement();
        Zoom();
    }

    void RotationConstrainMovement()
    {
        xPitch -= Input.GetAxis("Mouse Y") * speedRotation;
        xPitch = Mathf.Clamp(xPitch, yMin, yMax);
        yPitch -= Input.GetAxis("Mouse X")*-1 * speedRotation;
        yPitch = Mathf.Clamp(yPitch, xMin, xMax);
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
            if (cam.fieldOfView > minZoom) cam.fieldOfView -= modifier;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (cam.fieldOfView < maxZoom) cam.fieldOfView += modifier;
        }
    }

    public virtual void StartControl()
    {
        cam.enabled = true;
        isControlled = true;
    }

    public virtual void StopControl()
    {
        cam.enabled = false;
        isControlled = false;
    }
}
