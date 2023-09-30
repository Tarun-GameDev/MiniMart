using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectableObj : MonoBehaviour, ICollectable
{
    public ObjectType objectType = ObjectType.smallBox;
    public Storage _parentStorage;

    Vector3 targetPos;
    float smootSpeed = 3f;
    float weight = 0f;
    Vector3 startPos;
    bool moving = false;

    private void Start()
    {
        if(_parentStorage == null)
            _parentStorage = GetComponentInParent<Storage>();
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
        startPos = transform.localPosition;
        targetPos = _targetPos;
        moving = true;

        //rotation
        transform.rotation = _parentTran.transform.rotation;
    }

    public void Drop(GameObject _targetObj)
    {

    }
    #endregion



}
