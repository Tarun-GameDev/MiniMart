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
    public bool unloadingTruck = true;

    ControlManager controlManager;
    AudioManager audioManager;
    private void Start()
    {
        if (controlManager == null)
            controlManager = ControlManager.instance;

        if (audioManager == null)
            audioManager = AudioManager.instance;


        StartCoroutine(checkForLoadUnloadCompleted());
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

    IEnumerator checkForLoadUnloadCompleted()
    {
        if(underLoading)
        {
            if (unloadingTruck)
                checkForUnloadComplete();
            else
                CheckForLoadCompleted();
        }    

        yield return new WaitForSeconds(5f);

        StartCoroutine(checkForLoadUnloadCompleted());
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
        audioManager.Play("CashCollected");
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
