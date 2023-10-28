using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerObjectReceiver : UniversalObjectReceiver
{
    [SerializeField] Rig characterRig;
    bool rigActive = false;
    [SerializeField] VerificationCenter verificationCenter;
    protected new void Start()
    {
        base.Start(); //call the start function in UniversalObjectReceiver Class
        OnReceivingObjCalled += OnObjectReceiving;

        if (characterRig != null)
        {
            characterRig.weight = 0f;
            rigActive = false;
        }
    }

    #region Event Funtions
    public void OnObjectReceiving()
    {
        if (_thisStorage.StoredObj > 0)
            ActivateRigCheck();
        else
            DeactiveRigCheck();

        //call the verification center funtion to
        if(verificationCenter != null)
            verificationCenter.TryVerification();
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

        if (other.CompareTag("UnloadingSender") || other.CompareTag("VerificationSender"))
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
        if (other.CompareTag("UnloadingSender") || other.CompareTag("VerificationSender"))
        {
            DisableReceiveingObj();
        }
    }
    #endregion

    #region PlayerReceiver Specific Funitons
    public void ActivateRigCheck()
    {
        if (!rigActive) //check for already rig active and if objects added to playerStorage
        {
            if (characterRig != null)
            {
                characterRig.weight = 1f;
                rigActive = true;
            }
        }
    }

    public void DeactiveRigCheck()
    {
        if (rigActive)//check for already rig Deactive and if objects Removed to playerStorage
        {
            if (characterRig != null)
            {
                characterRig.weight = 0f;
                rigActive = false;
            }
        }
    }
    #endregion

}
