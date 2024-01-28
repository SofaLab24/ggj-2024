using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIController : MonoBehaviour
{
    public VisualElement shopRoot;
    public GameController gameController;
    public ImageOverlayController overlayController;
    public UIManager uiManager;

    public float item1Cost;
    public VisualElement item1Button;
    public float item2Cost;
    public VisualElement item2Button;
    public float item3Cost;
    public VisualElement item3Button;
    public float item4Cost;
    public VisualElement item4Button;

    private void Start()
    {
        shopRoot = GetComponent<UIDocument>().rootVisualElement.Q("ShopUI");
        gameController = FindFirstObjectByType<GameController>();
        overlayController = FindFirstObjectByType<ImageOverlayController>();
        uiManager = GetComponent<UIManager>();

        item1Button = shopRoot.Q("Item1").Q("Buy");
        item2Button = shopRoot.Q("Item2").Q("Buy");
        item3Button = shopRoot.Q("Item3").Q("Buy");
        item4Button = shopRoot.Q("Item4").Q("Buy");

        item1Button.RegisterCallback<ClickEvent>(OnItem1Buy);
        item2Button.RegisterCallback<ClickEvent>(OnItem2Buy);
        item3Button.RegisterCallback<ClickEvent>(OnItem3Buy);
        item4Button.RegisterCallback<ClickEvent>(OnItem4Buy);

    }

    public void OnItem1Buy(ClickEvent evt)
    {
        if (CheckPrice(item1Cost))
        {
            Debug.Log("nu");
            overlayController.DisplayImageOnMouse(overlayController.cigaretteImage);
            gameController.UpdateMoney(-item1Cost);
            uiManager.SetIsItemBoughtTrue();
        }
    }
    public void OnItem2Buy(ClickEvent evt)
    {
        if (CheckPrice(item2Cost))
        {
            overlayController.DisplayImageOnMouse(overlayController.beerImage);
            gameController.UpdateMoney(-item2Cost);
            uiManager.SetIsItemBoughtTrue();
        }
    }
    public void OnItem3Buy(ClickEvent evt)
    {
        if (CheckPrice(item3Cost))
        {
            overlayController.DisplayImageOnMouse(overlayController.vodkaImage);
            gameController.UpdateMoney(-item3Cost);
            uiManager.SetIsItemBoughtTrue();
        }
    }
    public void OnItem4Buy(ClickEvent evt)
    {
        if (CheckPrice(item4Cost))
        {
            overlayController.DisplayImageOnMouse(overlayController.bmwImage);
            gameController.UpdateMoney(-item4Cost);
            uiManager.SetIsItemBoughtTrue();
        }
    }

    private bool CheckPrice(float itemPrice)
    {
        Debug.Log("checking " + itemPrice + " <= " + gameController.money);
        Debug.Log(itemPrice <= gameController.money);
        return Mathf.Approximately(itemPrice, gameController.money) || itemPrice < gameController.money;
    }
}
