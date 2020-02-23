using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraHover : MonoBehaviour
{
    Image image;
    [SerializeField] Color notHoverColor;
    [SerializeField] Color hoverColor;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void Hover()
    {
        image.color = hoverColor;
    }

    public void UnHover()
    {
        image.color = notHoverColor;
    }
}
