using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckObjectReceiver : UniversalObjectReceiver
{
    [SerializeField] TruckController truckController;

    protected new void Start()
    {
        base.Start(); //call the start function in UniversalObjectReceiver Class
        OnReceivingObjCalled += OnObjectReceiving;
    }

    #region Event Funtions
    public void OnObjectReceiving()
    {
        //truckController.checkForUnloadComplete();
        truckController.CheckForLoadCompleted();
    }

    private void OnDestroy()
    {
        OnReceivingObjCalled += OnObjectReceiving;
    }
    #endregion

    #region Triggering Funitons
   
    private void OnTriggerEnter(Collider other)
    {
        if (receiverStorageFull)
            return;

        if (other.CompareTag("UnloadingSender"))
        {
            Storage _otherStorage = other.GetComponentInChildren<Storage>();
            
            if (_otherStorage != null)
            {
                TryReceiveObj(_otherStorage);
            }
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UnloadingSender"))
        {
            DisableReceiveingObj();
        }
    }
    public void SignalForReceivingObj(Storage _otherStorage)
    {
        TryReceiveObj(_otherStorage);
    }
    #endregion

}
