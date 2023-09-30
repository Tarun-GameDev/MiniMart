using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSender : MonoBehaviour
{
    [SerializeField] Storage _storage;

    [SerializeField]
    bool playerStorageFull = false;  //use for not asking every time when player triggered
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
        if (playerStorageFull)
            return;

        if(other.CompareTag("Player"))
        {
            playerIn = true;
            SendObject();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStorageFull = false;
            playerIn = false;
            model.transform.localScale = originalScale;
        }
    }

    void SendObject()
    {
        //ask for storage empty or not
        PlayerObjectRecSen _playerReceiver = LevelManager.instance.playerObjectRecSen;
        if (_playerReceiver != null)
        {
            //ask for storage
            if (!_playerReceiver._playerStorage.maxCountReached() && !_storage.minCountReached())//player storage is not full //so add objects to 
            {
                //visually look
                model.transform.localScale = newScale;

                //enable rig if it is disabled
                _playerReceiver.ActivateRigCheck();

                //player receive object                
                _playerReceiver._playerStorage.AddObj(_storage.RemoveObj());

                playerStorageFull = false;
            }
            else
            {
                playerStorageFull = true; //player storage is full so stop asking player
            }
        }

        StartCoroutine(sendObjeAgain());
    }

    IEnumerator sendObjeAgain()
    {
        yield return new WaitForSeconds(.1f);
        if (playerIn && !playerStorageFull)
        {
            SendObject();
        }

    }
}
