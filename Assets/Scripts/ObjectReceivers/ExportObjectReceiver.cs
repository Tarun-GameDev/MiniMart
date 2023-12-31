using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ExportObjectReceiver : UniversalObjectReceiver
{
    [SerializeField]
    PlayerObjectReceiver _playerReceiver;
    [SerializeField]
    TruckObjectReceiver truckObjectReceiver;

    protected new void Start()
    {
        base.Start(); //call the start function in UniversalObjectReceiver Class
        OnReceivingObjCalled += OnObjectReceiving;
    }

    #region Event Funtions
    void OnObjectReceiving()
    {
        if (_playerReceiver != null)
        {
            _playerReceiver.OnObjectReceiving();
        }

        StartCoroutine(giveSignal());
    }

    IEnumerator giveSignal()
    {
        yield return new WaitForSeconds(0.5f);
        truckObjectReceiver.SignalForReceivingObj(_thisStorage);
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

        if (other.CompareTag("PlayerSender"))
        {
            _playerReceiver = other.GetComponent<PlayerObjectReceiver>();
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
    }
    #endregion
}
