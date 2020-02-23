using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LayerMask projectileLayer;
    [SerializeField] Transform throwSource;
    [SerializeField] float maxXVelocity = 10;
    [SerializeField] float maxYVelocity = 5;
    [SerializeField] int lineSegment = 20;
    [SerializeField] Camera originCam;
    [SerializeField] GrabObject objectToThrow;
    float g;
    Vector3 vo;


    private void Awake()
    {
        g = Mathf.Abs(Physics2D.gravity.y);
        lineRenderer.positionCount = lineSegment;
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(g) * time;

        Vxz = Mathf.Clamp(Vxz, -maxXVelocity, maxXVelocity);
        Vy = Mathf.Clamp(Vy, -maxYVelocity, maxYVelocity);

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    public void Visualize()
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(i / (float)lineSegment);
            lineRenderer.SetPosition(i, pos);
        }
    }

    Vector3 CalculatePosInTime(float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = throwSource.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(g) * (time * time)) + (vo.y * time) + throwSource.position.y;

        result.y = sY;
        return result;
    }

    public void ShowRaycastProjectile()
    {
        lineRenderer.enabled = true;
        Ray camRay = originCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, projectileLayer))
        {
            vo = CalculateVelocity(hit.point, transform.position, 1f);
            throwSource.transform.rotation = Quaternion.LookRotation(vo);
            //objectToThrow.transform.rotation = Quaternion.LookRotation(vo);
            Visualize();
        }
    }

    public void HideRaycastProjectile()
    {
        lineRenderer.enabled = false;
    }

    public void Shoot()
    {
        objectToThrow.transform.parent = GameManager.instance.Elements;
        objectToThrow.rb.useGravity = true;
        objectToThrow.rb.isKinematic = false;
        objectToThrow.rb.velocity = vo;
        objectToThrow.col.enabled = true;
        objectToThrow = null;
    }


    public void SetObject(GrabObject grabObject)
    {
        objectToThrow = grabObject;
        throwSource = objectToThrow.transform;
    }
}
