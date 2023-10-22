using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public Vector3 PosAdder;
    public bool storeInGrid = false;
    public Vector3 VerticalPosAdder;
    public int maxVerticalStorage = 10; //no of objects can store vertically
    public Vector3 newPos;
    public int maxStorage = 3;
    public int StoredObj = 0;
    public List<CollectableObj> objects;
    public Dictionary<ObjectType, List<CollectableObj>> objectLists;

    [SerializeField]
    int _verticalStored = 1;

    private void Start()
    {
        CreateObjectLists();

        //adding objects to storage
        foreach (Transform child in transform) //goes through all the childrens object
        {
            CollectableObj _obj = child.GetComponent<CollectableObj>();
            if(_obj != null)
            {
                AddObj(_obj);

                if (maxStorage < StoredObj)
                    maxStorage = StoredObj;
            }
        }
    }

    #region Bools
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
    #endregion

    public Vector3 SetPositionAt()// return the new position to place object at positon
    {
        return newPos;
    }

    #region Adding Object
    public void AddObj(CollectableObj _obj)
    {
        _obj.PickUp(this.gameObject, newPos, this);
        StoredObj++;
        if (storeInGrid)
        {
            if(_verticalStored >= (maxVerticalStorage + 1))
            {
                newPos -= (PosAdder * maxVerticalStorage) ; //100  
                newPos += VerticalPosAdder;
                _verticalStored = 1;

            }
            else
            {
                newPos += PosAdder; //  000 001 002 100 101 102 200 201
                _verticalStored++;
            }
        }
        else
        {
            newPos += PosAdder;
        }
        objects.Add(_obj);

        //check the dictionay contains the specified objecttype
        if (objectLists.ContainsKey(_obj.objectType))
        {
            objectLists[_obj.objectType].Add(_obj);
        }
        else
            Debug.LogWarning("objectType not found in Dictionary");
    }
    #endregion

    #region RemovingObjects

    public void RemoveObj(CollectableObj _Obj)
    {
        StoredObj--;

        if (storeInGrid)
        {
            if (_verticalStored <= 0)
            {
                newPos += (PosAdder * maxVerticalStorage);
                newPos -= VerticalPosAdder;
                _verticalStored = maxVerticalStorage;
            }
            else
            {
                newPos -= PosAdder;
                _verticalStored--;
            }
        }
        else
        {
            newPos -= PosAdder;
        }

        objects.Remove(_Obj);
        RemoveObjFromDict(_Obj);
    }

    public void RemoveObjFromDict(CollectableObj _obj)
    {
        //check the dictionay contains the specified objecttype
        if (objectLists.ContainsKey(_obj.objectType))
        {
            objectLists[_obj.objectType].Remove(_obj);
        }
    }
    #endregion

    #region Useful Funtions

    void CreateObjectLists()
    {
        //create dictionary
        objectLists = new Dictionary<ObjectType, List<CollectableObj>>();

        foreach (ObjectType objectType in System.Enum.GetValues(typeof(ObjectType)))
        {
            objectLists.Add(objectType, new List<CollectableObj>());
        }
    }

    public bool IsListHasObjects(ObjectType _objectType)
    {
        return GetListByType(_objectType).Count > 0;
    }

    //access the list of typeObjects
    public List<CollectableObj> GetListByType(ObjectType objectType)
    {
        if (objectLists.ContainsKey(objectType))
        {
            return objectLists[objectType];
        }
        else
        {
            Debug.LogWarning("ObjectType not found in the dictionary");
            return new List<CollectableObj>();
        }
    }
    #endregion
}
