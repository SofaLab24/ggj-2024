using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private UIManager uiManager;
    public float money = 0;

    private void Start()
    {
        // PlayerPrefs.SetFloat("money", 0f);
        money = PlayerPrefs.GetFloat("money");
        Debug.Log(money);
        uiManager = FindFirstObjectByType<UIManager>();
        UpdateMoney(0);
    }

    public void UpdateMoney(float addMoney)
    {
        money += addMoney;
        uiManager.SetMoney(money);
        PlayerPrefs.SetFloat("money", money);
    }
}
