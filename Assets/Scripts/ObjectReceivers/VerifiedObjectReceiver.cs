using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class VerifiedObjectReceiver : UniversalObjectReceiver
{

    protected new void Start()
    {
        base.Start(); //call the start function in UniversalObjectReceiver Class
        OnReceivingObjCalled += OnObjectReceiving;
    }

    #region Event Funtions
    void OnObjectReceiving()
    {
        //call the verification center funtion to
    }

    private void OnDestroy()
    {
        OnReceivingObjCalled += OnObjectReceiving;
    }
    #endregion

    #region Triggering Funitons

    public void SignalForReceivingObj(Storage _otherStorage)
    {
        TryReceiveObj(_otherStorage);
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (receiverStorageFull)
            return;

        if (other.CompareTag("PlayerSender"))
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
        if (other.CompareTag("PlayerSender"))
        {
            DisableReceiveingObj();
        }
    }*/
    #endregion

    #region StoreRoom Specific Funitons

    #endregion

}
