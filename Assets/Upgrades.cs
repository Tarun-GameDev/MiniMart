using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    UpgradesManager upgradesManager;
    UpgradeItem item;

    private void Start()
    {
        upgradesManager = UpgradesManager.instance;
        if (item == null)
            item = GetComponent<UpgradeItem>();
    }

    public void DoublePlayerSpeed()
    {
        upgradesManager.DoublePlayerSpeed(item);
    }

    public void AddTruckStorage()
    {
        upgradesManager.BuyTruckStorage(item);
    }

    public void IncreasePlayercCapacity()
    {
        upgradesManager.BuyPlayerStorage(item);
    }

    public void IncreaseMainStorageCapa()
    {
        upgradesManager.BuyMainStorage(item);
    }
}
