using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : CameraController
{
    Rigidbody rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float robotRotationSpeed;
    [SerializeField] Transform hand = null;

    [HideInInspector] public GrabObject activeObject=null;
   // [SerializeField] GameObject save;

    Vector3 m_EulerAngleVelocity;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        Init();
        rb = GetComponent<Rigidbody>();
        m_EulerAngleVelocity = new Vector3(0, 100, 0);
    }

    void Update()
    {
        if (!isControlled) return;
        xStart = transform.eulerAngles.x;
        yStart = transform.eulerAngles.y;
        CameraCheck();
        MovementCheck();
        Throw();
    }

    void MovementCheck()
    {
        if (!isControlled) return;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        if(moveVertical!=0)
        {
            rb.MovePosition(transform.position + transform.forward * moveVertical * movementSpeed * Time.deltaTime);
        }
        if(moveHorizontal!=0)
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * moveHorizontal * Time.deltaTime * robotRotationSpeed);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

    public override void StartControl()
    {
        base.StartControl();
        //save.SetActive(false);
    }

    public override void StopControl()
    {
        base.StopControl();
        //save.SetActive(true);
    }

    public void Grab(GrabObject o)
    {
        if (activeObject != null) return;
        o.rb.useGravity = false;
        o.transform.parent = hand;
        o.transform.localPosition = new Vector3(0, 0, 0);
        o.transform.eulerAngles = new Vector3(0, 0, 0);
        o.rb.isKinematic = true;
        activeObject = o;

        UiManager.instance.GrabTextEnabled(false);
    }

    public void Throw()
    {
        if (activeObject == null || Input.GetAxisRaw("Throw")==0) return;
        activeObject.transform.parent = GameManager.instance.Elements;
        activeObject.rb.useGravity = true;
        activeObject.rb.isKinematic = false;
        activeObject.rb.AddForce(transform.forward*100);
        activeObject = null;
    }
}
