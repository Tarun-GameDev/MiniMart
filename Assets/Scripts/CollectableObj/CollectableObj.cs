using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectableObj : MonoBehaviour, ICollectable
{
    public ObjectType objectType = ObjectType.smallBox;
    [HideInInspector]
    public Storage _parentStorage;

    Vector3 targetPos;
    float smootSpeed = 3f;
    float weight = 0f;
    Vector3 startPos;
    public GameObject verificationModel;
    [HideInInspector]
    public bool moving = false;
    public bool Verified = false;

    private void Start()
    {
        weight = 0f;

        if(_parentStorage == null)
            _parentStorage = GetComponentInParent<Storage>();

        startPos = transform.localPosition;

        //SetVerification(false);
    }

    private void Update()
    {
        //move Object smoothly to the targetPos
        if(moving)
        {
            if(transform.localPosition == targetPos)//position reached
            {
                moving = false; //stop moving
                return;
            }
            else
            {
                weight += Time.deltaTime * smootSpeed;
                transform.localPosition = Vector3.Lerp(startPos, targetPos, weight);
            }
        }
    }


    #region Icollectable Funtions
    public void PickUp(GameObject _parentTran, Vector3 _targetPos, Storage _newStorage)
    {
        //changing the 
        transform.SetParent(_parentTran.transform);

        //move object smoothly
        MoveObject(_targetPos);

        //rotation
        transform.rotation = _parentTran.transform.rotation;
    }

    public void SetVerification(bool _verified)
    {
        if (verificationModel != null)
            verificationModel.SetActive(_verified);
        Verified = _verified;
    }
    #endregion

    public void MoveObject(Vector3 _newPos)
    {
        weight = 0f;
        targetPos = _newPos;
        startPos = transform.localPosition;
        moving = true;
    }

}
