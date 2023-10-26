using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTrucksController : MonoBehaviour
{
    [SerializeField] TruckController[] trucks;

    private void Start()
    {
        StartCoroutine(SpawnTruck());
    }

    IEnumerator SpawnTruck()
    {
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < trucks.Length; i++)
        {
            if(!trucks[i].underLoading)
            {
                trucks[i].StartUnload();
                break;
            }            
        }

        StartCoroutine(SpawnTruck());

    }

    //check for other active trucks

    //spawn trucks based on time interval

    //spawn trucks wherver empty found

    //spawn loading truck and unloading truck simltaneoul

}
