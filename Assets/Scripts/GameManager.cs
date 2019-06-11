using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraController activeCamera;
    List<Camera> listCam;
    [SerializeField] Transform listener;
    [SerializeField] public Transform Elements;

    public static GameManager instance = null;

    CameraController firstCamera;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        firstCamera = activeCamera;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        listener.parent = activeCamera.transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetCam();
        }
    }

    void ResetCam()
    {
        SelectCamera(firstCamera);
    }

    public void SelectCamera(CameraController c)
    {
        activeCamera.StopControl();
        activeCamera = c;
        listener.parent = c.transform;
        c.StartControl();
    }
}
