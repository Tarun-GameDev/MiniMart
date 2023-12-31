using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UniversalObjectReceiver : MonoBehaviour
{
    public Storage _thisStorage;
    [Header("Tick If the receiver can store specific type or anytype of objects")]
    public bool storeSpecificType = false;
    public ObjectType objectType = ObjectType.BigBox;
    [Header("Tick If the receiver need to check verified or not")]
    public bool checkForVerification = true;
    public bool NeedVerification = false;//Need verificatin for unloading boxes and not need verification for storing in room
    public bool receiverStorageFull = false;  //use for not asking every time when player triggered
    [HideInInspector]
    public bool playerIn = false; //tells whether player is still in trigger range or not

    public GameObject model;
    [HideInInspector]
    public Vector3 newScale = new Vector3(1.3f, 1f, 1.3f);
    [HideInInspector]
    public Vector3 originalScale;

    public event Action OnReceivingObjCalled;
    public UniversalObjectReceiver otherObjectReceiver;

    Storage otherStorage;
    int prevIndex = 0;
    int indexDecrement = 0;

    protected void Start()
    {
        if (model != null)
            originalScale = model.transform.localScale;
        playerIn = false;
    }

    public void TryReceiveObj(Storage _otherStorage)
    {
        playerIn = true;

        //if (storeSpecificType)//if it stores specific type of objects 
        //    noOfObjectsPresent = _otherStorage.ObjectCountInDic(objectType);

        prevIndex = 0;
        indexDecrement = 0;
        otherStorage = _otherStorage;
        ReceiveObj(_otherStorage);
    }

    public void RecheckReceiveObj()
    {
        if(playerIn)
            TryReceiveObj(otherStorage);
    }

    public void DisableReceiveingObj()
    {
        receiverStorageFull = false;
        playerIn = false;
        otherStorage = null;
        if (model != null)
            model.transform.localScale = originalScale;
    }

    public void ReceiveObj(Storage _otherStorage)
    {
        //Debug.Log(gameObject.name);
        //ask for storage empty or not
        //PlayerObjectRecSen _playerReceiver = LevelManager.instance.playerObjectRecSen;
        if (!_otherStorage.minCountReached() && !_thisStorage.maxCountReached())
        {
            //Debug.Log("flkakehoiauh");
            if(storeSpecificType) // stores specific type
            {
                if (_otherStorage.IsListHasObjects(objectType))
                {
                    ReceiveObjBy(_otherStorage.GetListByType(objectType), _otherStorage);
                }
                else
                {
                    receiverStorageFull = true;
                }
            }
            else //stores all types of objects
            {
                if(_otherStorage.objects.Count > 0) //which means list contians objects
                {
                    ReceiveObjBy(_otherStorage.objects, _otherStorage);
                }
                else
                {
                    receiverStorageFull = true;
                }
            }
        }
        else
        {
            //check for next object
            receiverStorageFull = true;
        }
        StartCoroutine(ReceiveObjAgain(_otherStorage));
    }

    void ReceiveObjBy(List<CollectableObj> _list,Storage _otherStorage)
    {

        if (checkForVerification) //if Verification needed
        {
            prevIndex = _list.Count - 1 - indexDecrement;

            if (prevIndex >= 0)
            {

                CollectableObj _collectableObj = _list[prevIndex];

                if (NeedVerification == _collectableObj.Verified)
                {
                    //add object to this storage
                    _otherStorage.RemoveObj(_collectableObj);
                    _thisStorage.AddObj(_collectableObj);

                    OnReceivingObjCalled?.Invoke();
                    if (otherObjectReceiver != null)
                        otherObjectReceiver.RecheckReceiveObj();
                }
                else //which means 
                {
                    indexDecrement++;
                    ReceiveObj(_otherStorage);
                }
            }
            else
            {
                receiverStorageFull = true;
            }

        }
        else //not need for verificatoin like for player storage
        {
            prevIndex = _list.Count - 1;

            if (prevIndex >= 0)
            {
                CollectableObj _collectableObj = _list[prevIndex];

                //add object to this storage
                _otherStorage.RemoveObj(_collectableObj);
                _thisStorage.AddObj(_collectableObj);


                OnReceivingObjCalled?.Invoke();
                if (otherObjectReceiver != null)
                    otherObjectReceiver.RecheckReceiveObj();
            }
            else
            {
                receiverStorageFull = true;
            }


        }
    }

    IEnumerator ReceiveObjAgain(Storage _otherStorage)
    {
        yield return new WaitForSeconds(.1f);
        if (playerIn && !receiverStorageFull)
        {
            ReceiveObj(_otherStorage);
        }
    }
}
