using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIController : MonoBehaviour
{
    public VisualElement shopRoot;
    public GameController gameController;

    public VisualElement item1Button;
    public VisualElement item2Button;
    public VisualElement item3Button;
    public VisualElement item4Button;

    private void Start()
    {
        shopRoot = GetComponent<UIDocument>().rootVisualElement.Q("ShopUI");
        gameController = FindFirstObjectByType<GameController>();

        item1Button = shopRoot.Q("Item1").Q("Buy");
        item2Button = shopRoot.Q("Item2").Q("Buy");
        item3Button = shopRoot.Q("Item3").Q("Buy");
        item4Button = shopRoot.Q("Item4").Q("Buy");
    }
}
