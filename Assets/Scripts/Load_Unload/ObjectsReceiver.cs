using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsReceiver : MonoBehaviour
{
    [SerializeField] Storage _storage;

    [SerializeField]
    bool receiverStorageFull = false;  //use for not asking every time when player triggered
    [SerializeField]
    bool playerIn = false; //tells whether player is still in trigger range or not

    [SerializeField] GameObject model;
    Vector3 newScale = new Vector3(1.2f, 1.2f, 3.2f);
    Vector3 originalScale;


    private void Start()
    {
        originalScale = model.transform.localScale;
        playerIn = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (receiverStorageFull)
            return;

        if (other.CompareTag("Player"))
        {
            playerIn = true;
            ReceiveObj();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            receiverStorageFull = false;
            playerIn = false;
            model.transform.localScale = originalScale;
        }
    }

    void ReceiveObj()
    {
        //ask for storage empty or not
        PlayerObjectRecSen _playerReceiver = LevelManager.instance.playerObjectRecSen;
        if (_playerReceiver != null)
        {
            //ask for storage
            if (!_playerReceiver._playerStorage.minCountReached() && !_storage.maxCountReached())//receiver storage is not full //so add objects to receiver
            {
                //visually look
                model.transform.localScale = newScale;

                //enable rig if it is disabled
                _playerReceiver.DeactiveRigCheck();

                //receiveer receive object                
                _storage.AddObj(_playerReceiver._playerStorage.RemoveObj());

                receiverStorageFull = false;
            }
            else
            {
                receiverStorageFull = true; //player storage is full so stop asking player
            }
        }

        StartCoroutine(ReceiveObjAgain());
    }

    IEnumerator ReceiveObjAgain()
    {
        yield return new WaitForSeconds(.1f);
        if (playerIn && !receiverStorageFull)
        {
            ReceiveObj();
        }

    }
}
