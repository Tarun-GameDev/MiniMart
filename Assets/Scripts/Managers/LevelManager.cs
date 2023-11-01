using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public UIManager uiManager;

    public PlayerController player;
    public int CollectdCash = 0;
    public Storage playerStorage;

    private void Awake()
    {
        PlayerPrefs.SetInt("Cash", 1000);
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();

        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();

        CollectdCash = PlayerPrefs.GetInt("Cash", 0);
        uiManager.SetCashText(CollectdCash);
    }

    public void AddMoney(int _amount)
    {
        CollectdCash += _amount;
        PlayerPrefs.SetInt("Cash", CollectdCash);
        uiManager.SetCashText(CollectdCash);
    }

    public void RemoveMoney(int _amount)
    {
        CollectdCash -= _amount;
        PlayerPrefs.SetInt("Cash", CollectdCash);
        uiManager.SetCashText(CollectdCash);
    }
}
