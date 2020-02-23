using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableIcon : MonoBehaviour
{

    Sprite baseIcon;
    [SerializeField] Sprite hoverIcon;
    [SerializeField] Canvas documentCanvas;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        baseIcon = image.sprite;
    }

    public void OnMouseEnter()
    {
        image.sprite = hoverIcon;
    }

    public void OnMouseExit()
    {
        image.sprite = baseIcon;
    }

    public void OpenDocumentCanvas()
    {
        documentCanvas.gameObject.SetActive(true);
    }

    public void CloseDocumentCanvas()
    {
        image.sprite = baseIcon;
        documentCanvas.gameObject.SetActive(false);
    }

}
