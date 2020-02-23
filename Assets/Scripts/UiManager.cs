using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;
    [SerializeField] Text grabText;
    public Canvas laptopCanvas;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GrabTextEnabled(false);
    }

    public void GrabTextEnabled(bool b)
    {
        grabText.enabled = b;
    }

    public void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EnableLaptopCanvas()
    {
        laptopCanvas.gameObject.SetActive(true);
    }

    public void DisableLaptopCanvas()
    {
        laptopCanvas.gameObject.SetActive(false);
    }
}
