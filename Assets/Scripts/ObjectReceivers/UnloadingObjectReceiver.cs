using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class UnloadingObjectReceiver : UniversalObjectReceiver
{
    protected new void Start()
    {
        base.Start(); //call the start function in UniversalObjectReceiver Class
        OnReceivingObjCalled += OnObjectReceiving;
    }

    #region Event Funtions
    public void OnObjectReceiving()
    {

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

        if (other.CompareTag("UnloadingTruck"))
        {
            Debug.Log("Truck Entered");
            Storage _otherStorage = other.GetComponentInChildren<Storage>();
            if (_otherStorage != null)
            {
                TryReceiveObj(_otherStorage);
            }
                
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UnloadingTruck"))
        {
            DisableReceiveingObj();
        }
    }
    #endregion


}
