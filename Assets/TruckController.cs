using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CollectableObj objectPrefab;
    [SerializeField] Storage truckStorage;
    [HideInInspector]
    public bool underLoading = false;

    public void StartUnload()
    {
        underLoading = true;
        SpawnObjects();
        animator.SetTrigger("MoveForward");
        //spawn object to truck and add them to storage
        //move the truck
        //once reached 
        //start unload (automatic)
        //
    }

    void SpawnObjects()
    {
        for (int i = 0; i < truckStorage.maxStorage; i++)
        {
            CollectableObj _obj = Instantiate(objectPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<CollectableObj>();
            if (_obj != null)
            {
                truckStorage.AddObj(_obj);
            }
        }
    }

    public void checkForUnloadComplete()
    {
        if (truckStorage.minCountReached())//all objects are unloaded
        {
            StartCoroutine(UnloadedCompleted());
           
        }
    }

    IEnumerator UnloadedCompleted()
    {
        animator.SetTrigger("MoveBackward");
        yield return new WaitForSeconds(2f);
        underLoading = false;
    }
}
