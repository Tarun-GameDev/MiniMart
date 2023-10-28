using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTrucksController : MonoBehaviour
{
    [SerializeField] bool UnloadTrucks = true;
    [SerializeField] TruckController[] trucks;

    private void Start()
    {
        trucks = GetComponentsInChildren<TruckController>();

        StartCoroutine(SpawnTruck());
    }

    IEnumerator SpawnTruck()
    {

        for (int i = 0; i < trucks.Length; i++)
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

    //check for other active trucks

    //spawn trucks based on time interval

    //spawn trucks wherver empty found

    //spawn loading truck and unloading truck simltaneoul

}
