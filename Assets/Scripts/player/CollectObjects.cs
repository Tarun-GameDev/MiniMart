using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CollectObjects : MonoBehaviour
{
    [SerializeField] Transform ObjectPivot;
    [SerializeField] int objectsCollected = 0;
    [SerializeField] GameObject pizzaBoxPrefab;
    [SerializeField] List<GameObject> ObjectsArray = new List<GameObject>();

    [SerializeField] Rig characterRig;

    private void Start()
    {
        if (characterRig != null)
            characterRig.weight = 0f;

        objectsCollected = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CollectableObject"))
        {
            AddBox(other.gameObject);
        }

    }
    public void AddBox(GameObject _object)
    {
        SetObjectPos(_object);

        if (objectsCollected >= 1)
        {
            if (characterRig != null)
                characterRig.weight = 1f;
        }
    }

    void SetObjectPos(GameObject _box)
    {
        _box.tag = "CollectedBox";
        _box.transform.SetParent(ObjectPivot);
        _box.transform.localPosition = new Vector3(0f, objectsCollected * .2f, 0f);
        _box.transform.rotation = Quaternion.Euler(Vector3.zero);
        ObjectsArray.Add(_box.gameObject);

        objectsCollected++;
    }

    public void RemoveBox(int _noOfBoxes)
    {
        if (objectsCollected >= 1)
        {
            for (int i = 0; i < _noOfBoxes; i++)
            {
                if (objectsCollected >= 1)
                {
                    Destroy(ObjectsArray[objectsCollected - 1]);
                    ObjectsArray.RemoveAt(objectsCollected - 1);
                    objectsCollected--;
                }
            }
        }

    }



    public void EnbaleBoxPhysics(int _indexFrom)
    {
        if (objectsCollected <= 0)
            return;

        for (int i = objectsCollected - 1; i >= _indexFrom; i--)
        {
            GameObject _box = ObjectsArray[i];

            _box.transform.parent = null;
            _box.GetComponent<Collider>().isTrigger = false;
            _box.GetComponent<Rigidbody>().isKinematic = false;
            //Destroy(_box, 3f);
            ObjectsArray.RemoveAt(i);
            objectsCollected--;
        }
    }


    public void ReleaseObject(GameObject _delivaryPos, int _noOfPizzas)
    {
        if (objectsCollected >= 1)
        {
            for (int i = 1; i <= _noOfPizzas; i++)
            {
                if (objectsCollected >= 1)
                {
                    GameObject _box = ObjectsArray[ObjectsArray.Count - 1];
                    ObjectsArray.RemoveAt(ObjectsArray.Count - 1);
                    _box.transform.parent = null;
                    _box.GetComponent<Object>().moveTowardsPos(_delivaryPos);
                    objectsCollected--;
                }

            }

        }
    }

    public void RemoveAllPizzas()
    {
        EnbaleBoxPhysics(0);
    }

}
