using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificationCenter : MonoBehaviour
{
    public Storage nonverifiedStorage;
    public Storage verificationStorage;
    public Storage verifiedStorage;
    [SerializeField] VerifiedObjectReceiver verifiedObjectReceiver;

    public bool stillVerifying = false;
    //check for verification

    //get signal from nonverified storage whenever new object is added in nonverified storage
    //get signal from verified storage whenever object is removed from verified storage

    public void TryVerification()
    {
        if (!stillVerifying)
            CheckForStorage();
    }

    void CheckForStorage()
    {
        if (!nonverifiedStorage.minCountReached() && !verifiedStorage.maxCountReached()) //check for not verified storage has atlesat one object and verified storage has space
        {
            StartCoroutine(changeVerification());
        }
        else
        {
            stillVerifying = false;
        }
        //check for maxstorage for 
    }

    IEnumerator changeVerification()
    {
        //get the object from verifcation storage
        //move the object to this pos
        //change the object to verified
        //move the object to verified storage
        stillVerifying = true;

        yield return new WaitForSeconds(.2f);
        CollectableObj _obj = nonverifiedStorage.objects[nonverifiedStorage.objects.Count - 1]; //get last element on storage
        nonverifiedStorage.RemoveObj(_obj);
        verificationStorage.AddObj(_obj);
       
        //play char stamp animation
        yield return new WaitForSeconds(.3f);

        _obj.SetVerification(true);


        yield return new WaitForSeconds(.5f);
        //send signal to verif
        verifiedObjectReceiver.SignalForReceivingObj(verificationStorage);

        CheckForStorage();
    }
}
