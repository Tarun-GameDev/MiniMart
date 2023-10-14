using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class VerificationObjectReceiver : UniversalObjectReceiver
{
    [SerializeField]
    WorkerObjectReceiver _workerReceiver;

    protected new void Start()
    {
        base.Start(); //call the start function in UniversalObjectReceiver Class
        OnReceivingObjCalled += OnObjectReceiving;
    }

    #region Event Funtions
    void OnObjectReceiving()
    {
        if(_workerReceiver != null)
        {
            _workerReceiver.OnObjectReceiving();
        }
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

        if (other.CompareTag("WorkerSender"))
        {
            _workerReceiver =  other.GetComponent<WorkerObjectReceiver>();
            Storage _otherStorage = other.GetComponentInChildren<Storage>();
            
            if (_otherStorage != null)
            {
                TryReceiveObj(_otherStorage);
            }
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WorkerSender"))
        {
            DisableReceiveingObj();
        }
    }
    #endregion

    #region StoreRoom Specific Funitons

    #endregion

}
