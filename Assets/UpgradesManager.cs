using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    PlayerController playerController;
    LevelManager levelManager;
    [SerializeField] AllTrucksController allTrucksControllers;
    Storage playerStorage;
    [SerializeField] Storage mainStorage;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        levelManager = LevelManager.instance;
        playerController = LevelManager.instance.player;
        if (playerStorage == null)
        {
            playerStorage = levelManager.playerStorage;
            playerStorage.maxStorage = PlayerPrefs.GetInt("PlayerStorageCapacity",3);
        }
        if(mainStorage != null)
        {
            mainStorage.maxStorage = PlayerPrefs.GetInt("MainStorageCapacity", 9);
        }
    }

    #region playerSpeed
    public void DoublePlayerSpeed(UpgradeItem _item)
    {
        if (canBuy(_item.buyPrice,true))
        {
            playerController.moveSpeed *= 2f;
            StartCoroutine(DisablePlayerSpeed(_item));
        }
    }

    IEnumerator DisablePlayerSpeed(UpgradeItem _item)
    {
        _item.DisableButton();
        yield return new WaitForSeconds(300f);
        _item.EnableButton();
        playerController.moveSpeed /= 2f;
    }
    #endregion

    public void BuyPlayerStorage(UpgradeItem _item)
    {
        //check for maxReached
        if(PlayerPrefs.GetInt("PlayerStorageCapacity", 2) <= 8)
        {
            if (canBuy(_item.buyPrice, true))
            {
                playerStorage.maxStorage += 2;
                PlayerPrefs.SetInt("PlayerStorageCapacity", playerStorage.maxStorage);
            }
        }
        else
        {
            _item.DisableButton();
        }
    }

    public void BuyTruckStorage(UpgradeItem _item)
    {
        if(canBuy(_item.buyPrice,false))
        {
            if(allTrucksControllers.CanAddTruckStorage())
            {
                canBuy(_item.buyPrice, true);
            }
            else
            {
                _item.DisableButton();
            }
        }
    }

    public void BuyMainStorage(UpgradeItem _item)
    {
        if(mainStorage.maxStorage <= 200)
        {
            if (canBuy(_item.buyPrice, true))
            {
                mainStorage.maxStorage += 10;
                PlayerPrefs.SetInt("MainStorageCapacity", mainStorage.maxStorage);
            }
            
        }
        else
        {
            _item.DisableButton();
        }

    }

    public bool canBuy(int _dollars, bool buy)
    {
        bool _canBuy = false;
        if (levelManager.CollectdCash >= _dollars)
        {
            //has enough cash
            _canBuy = true;
            if(buy)
                levelManager.RemoveMoney(_dollars);
        }
        else
        {
            //display not enough money
            _canBuy = false;
        }
        return _canBuy;
    }
}
