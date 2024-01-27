using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    //main docs
    UIDocument mainScreen;
    VisualElement mainMenuUIRoot;
    VisualElement shopUIRoot;
    VisualElement miniGamesUIRoot;

    //window buttons
    VisualElement openShopButton;
    VisualElement closeShopButton;
    VisualElement openMiniGamesButton;
    VisualElement closeMiniGamesButton;

    //money labels
    Label mainMenuMoney;
    Label shopMoney;

    //idle maroz animation
    public Texture2D marozStanding;
    public Texture2D marozSitting;
    VisualElement maroz;

    public float timeBetweenIdle = 2f;
    float timeLeft;
    private bool isMarozStanding;

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

        mainMenuMoney = mainMenuUIRoot.Q<Label>("Money");
        shopMoney = shopUIRoot.Q<Label>("Money");

        maroz = mainMenuUIRoot.Q("Marozas");
        timeLeft = timeBetweenIdle;
        isMarozStanding = true;
    }

    public void Update()
    {
        if (timeLeft <= 0)
        {
            if(isMarozStanding)
            {
                maroz.style.backgroundImage = marozSitting;
                isMarozStanding = false;
                timeLeft = timeBetweenIdle;
            }
            else
            {
                maroz.style.backgroundImage = marozStanding;
                isMarozStanding = true;
                timeLeft = timeBetweenIdle;
            }
        }
        timeLeft -= Time.deltaTime;
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

    public void SetMoney(float money)
    {
        mainMenuMoney.text = money + " Lt";
        shopMoney.text = money + " Lt";
    }
}
