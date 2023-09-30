using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public Vector3 PosAdder;
    [SerializeField] Vector3 newPos;
    public int maxStorage = 3;
    public int StoredObj = 0;
    public List<CollectableObj> objects;

    private void Start()
    {
        //adding objects to storage
        foreach (Transform child in transform) //goes through all the childrens object
        {
            CollectableObj _obj = child.GetComponent<CollectableObj>();
            if(_obj != null && StoredObj < maxStorage)
            {
                objects.Add(_obj);
                StoredObj++;
                newPos += PosAdder;
            }
        }
    }

    public bool maxCountReached()//check storage is full or not //use in adding objects
    {
        if (StoredObj == maxStorage)
            return true;
        else
            return false;
    }

    public bool minCountReached()//check storage is empty or not //use in removing objects
    {
        if (StoredObj == 0)
            return true;
        else
            return false;
    }

    public Vector3 SetPositionAt()// return the new position to place object at positon
    {
        return newPos;
    }

    public void AddObj(CollectableObj _obj)
    {
        _obj.PickUp(this.gameObject, newPos, this);
        StoredObj++;
        newPos += PosAdder;
        objects.Add(_obj);
    }

    public CollectableObj RemoveObj()
    {
        //whenever objects are removed from storage
        StoredObj--;
        newPos -= PosAdder;
        CollectableObj _removedObj = objects[0];
        objects.Remove(_removedObj);
        return _removedObj;
    }
}
