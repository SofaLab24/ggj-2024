using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ImageOverlayController : MonoBehaviour
{
    public Texture2D cigaretteImage;
    public Texture2D beerImage;
    public Texture2D bmwImage;
    public Texture2D vodkaImage;

    public VisualElement overlayImage;

    Texture2D currentSelected;
    bool displayImageOnMouse = false;

    public void Start()
    {
        overlayImage = GetComponent<UIDocument>().rootVisualElement.Q("ItemImage");
        overlayImage.pickingMode = PickingMode.Ignore;

        //---FOR DEBUGGING--------------
        DisplayImageOnMouse(beerImage);
        //------------------------------
    }

    public void Update()
    {
        if (displayImageOnMouse)
        {
            Vector2 pos = Input.mousePosition;
            overlayImage.style.bottom = pos.y / 3;
            overlayImage.style.left = pos.x / 3;
        }
    }

    public void DisplayImageOnMouse(Texture2D image)
    {
        currentSelected = image;
        overlayImage.style.backgroundImage = currentSelected;
        displayImageOnMouse = true;
    }
    public void StopDisplayingImageOnMouse()
    {
        displayImageOnMouse = false;
    }
}
