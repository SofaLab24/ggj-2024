using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private UIManager uiManager;
    public float money = 0;

    private void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
    }

    public void UpdateMoney(float addMoney)
    {
        money += addMoney;
        uiManager.SetMoney(money);
    }
}
