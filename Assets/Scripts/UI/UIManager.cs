using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    //sfx
    public List<AudioClip> jokes;
    AudioSource audioSource;

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

    //maroz looking at item
    public Texture2D marozClosedEyes;
    public bool isItemBought;

    ImageOverlayController imageOverlay;
    public void Awake()
    {
        mainScreen = GetComponent<UIDocument>();

        mainMenuUIRoot = mainScreen.rootVisualElement.Q("MainMenuUI");
        shopUIRoot = mainScreen.rootVisualElement.Q("ShopUI");
        mainMenuMoney = mainMenuUIRoot.Q<Label>("Money");
        shopMoney = shopUIRoot.Q<Label>("Money");

    }
    public void Start()
    {
        audioSource = FindFirstObjectByType<AudioSource>();

        miniGamesUIRoot = mainScreen.rootVisualElement.Q("MiniGamesUI");

        openShopButton = mainMenuUIRoot.Q("OpenShop");
        openShopButton.RegisterCallback<ClickEvent>(OnShopOpen);
        openMiniGamesButton = mainMenuUIRoot.Q("OpenMiniGames");
        openMiniGamesButton.RegisterCallback<ClickEvent>(OnMiniGamesOpen);

        closeShopButton = shopUIRoot.Q("Close");
        closeShopButton.RegisterCallback<ClickEvent>(OnShopClose);
        closeMiniGamesButton = miniGamesUIRoot.Q("Close");
        closeMiniGamesButton.RegisterCallback<ClickEvent>(OnMiniGamesClose);

        maroz = mainMenuUIRoot.Q("Marozas");
        maroz.pickingMode = PickingMode.Ignore;
        maroz.RegisterCallback<ClickEvent>(OnMarozClick);
        timeLeft = timeBetweenIdle;
        isMarozStanding = true;
        isItemBought = false;

        imageOverlay = FindFirstObjectByType<ImageOverlayController>();
    }

    public void Update()
    {
        if (timeLeft <= 0)
        {
            if (!isItemBought)
            {
                if (isMarozStanding)
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
            else
            {
                if (isMarozStanding)
                {
                    maroz.style.backgroundImage = marozSitting;
                    isMarozStanding = false;
                    timeLeft = timeBetweenIdle;
                }
                else
                {
                    maroz.style.backgroundImage = marozClosedEyes;
                    isMarozStanding = true;
                    timeLeft = timeBetweenIdle;
                }
            }
        }

        timeLeft -= Time.deltaTime;
    }
    public void SetIsItemBoughtTrue()
    {
        isItemBought = true;
        maroz.pickingMode = PickingMode.Position;
    }
    public void OnMarozClick(ClickEvent evt)
    {
        //AUDIO MANAGER, LAUNCH INGA SOUND
        imageOverlay.StopDisplayingImageOnMouse();
        maroz.pickingMode = PickingMode.Ignore;
        isItemBought = false;
        audioSource.clip = jokes[Random.Range(0, jokes.Count)];
        audioSource.Play();
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
