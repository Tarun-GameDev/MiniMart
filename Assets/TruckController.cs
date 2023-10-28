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
    public int cashAmountForDelivary = 3;

    ControlManager controlManager;

    private void Start()
    {
        if (controlManager == null)
            controlManager = ControlManager.instance;
    }

    public void StartUnload()
    {
        underLoading = true;
        SpawnObjects();
        animator.SetTrigger("MoveBackward");
    }

    public void StartLoad()
    {
        underLoading = true;
        animator.SetTrigger("MoveBackward");
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

    public void CheckForLoadCompleted()
    {
        if(truckStorage.maxCountReached()) //objects are loaded into truck and ready for shipping
        {
            StartCoroutine(loadCompleted());
        }
    }

    public void checkForUnloadComplete()
    {
        if (truckStorage.minCountReached())//all objects are unloaded
        {
            StartCoroutine(UnloadedCompleted());
        }
    }

    IEnumerator loadCompleted()
    {
        animator.SetTrigger("MoveForward");
        for (int i = 0; i < cashAmountForDelivary; i++)
        {
            controlManager.spawnCash();
        }
        yield return new WaitForSeconds(2f);
        truckStorage.RemoveAllObjects();
        underLoading = false;
    }

    IEnumerator UnloadedCompleted()
    {
        animator.SetTrigger("MoveForward");
        yield return new WaitForSeconds(2f);
        underLoading = false;
    }
}
