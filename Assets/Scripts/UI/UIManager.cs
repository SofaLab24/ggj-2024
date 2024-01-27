using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    UIDocument mainScreen;
    VisualElement mainMenuUIRoot;
    VisualElement shopUIRoot;
    VisualElement miniGamesUIRoot;

    VisualElement openShopButton;
    VisualElement closeShopButton;
    VisualElement openMiniGamesButton;
    VisualElement closeMiniGamesButton;

    public void Start()
    {
        mainScreen = GetComponent<UIDocument>();

        mainMenuUIRoot = mainScreen.rootVisualElement.Q("MainMenuUI");
        shopUIRoot = mainScreen.rootVisualElement.Q("ShopUI");
        miniGamesUIRoot = mainScreen.rootVisualElement.Q("MiniGamesUI");

        openShopButton = mainMenuUIRoot.Q("OpenShop");
        openShopButton.RegisterCallback<ClickEvent>(OnShopOpen);
        openMiniGamesButton = mainMenuUIRoot.Q("OpenMiniGames");
        openMiniGamesButton.RegisterCallback<ClickEvent>(OnMiniGamesOpen);

        closeShopButton = shopUIRoot.Q("Close");
        closeShopButton.RegisterCallback<ClickEvent>(OnShopClose);
        closeMiniGamesButton = miniGamesUIRoot.Q("Close");
        closeMiniGamesButton.RegisterCallback<ClickEvent>(OnMiniGamesClose);

    }

    public void OnShopOpen(ClickEvent evt)
    {
        mainMenuUIRoot.style.opacity = 0.3f;
        shopUIRoot.style.display = DisplayStyle.Flex;
    }
    public void OnShopClose(ClickEvent evt)
    {
        shopUIRoot.style.display = DisplayStyle.None;
        mainMenuUIRoot.style.opacity = 1f;
    }
    public void OnMiniGamesOpen(ClickEvent evt)
    {
        mainMenuUIRoot.style.opacity = 0.3f;
        miniGamesUIRoot.style.display = DisplayStyle.Flex;
    }
    public void OnMiniGamesClose(ClickEvent evt)
    {
        miniGamesUIRoot.style.display = DisplayStyle.None;
        mainMenuUIRoot.style.opacity = 1f;
    }
}
