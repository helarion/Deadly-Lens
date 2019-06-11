using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;
    [SerializeField] Text grabText;

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
}
