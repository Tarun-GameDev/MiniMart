using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTrucksController : MonoBehaviour
{
    [SerializeField] bool UnloadTrucks = true;
    [SerializeField] List<TruckController> AllTrucks;
    [SerializeField] List<GameObject> AllTrucksGameobj;
    [SerializeField]
    List<TruckController> trucks;    
    [SerializeField]
    int unlockedTrucks = 1;
    [SerializeField] AllTrucksController loadingTruckController;

    private void Start()
    {
        unlockedTrucks = PlayerPrefs.GetInt("UnlockedTruckSotrage", 1);

        for (int i = 0; i < unlockedTrucks; i++)
        {
            trucks.Add(AllTrucks[i]);
            AllTrucksGameobj[i].gameObject.SetActive(true);
        }
        StartCoroutine(SpawnTruck());
    }

    IEnumerator SpawnTruck()
    {
        for (int i = 0; i < trucks.Count; i++)
        {
            if(!trucks[i].underLoading)
            {
                if (UnloadTrucks)
                    trucks[i].StartUnload();
                else
                    trucks[i].StartLoad();
                break;
            }            
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnTruck());
    }

    public bool CanAddTruckStorage()
    {
        bool canAdd = false;
        if (unlockedTrucks < AllTrucks.Count)
        {
            canAdd = true;
            unlockedTrucks++;
            if(UnloadTrucks)
                PlayerPrefs.SetInt("UnlockedTruckSotrage", unlockedTrucks);
            AllTrucksGameobj[unlockedTrucks-1].SetActive(true);
            trucks.Add(AllTrucks[unlockedTrucks]);
            if (loadingTruckController != null)
                loadingTruckController.CanAddTruckStorage();
        }
        else
        {
            canAdd = false;
        }

        return canAdd;
    }
}
