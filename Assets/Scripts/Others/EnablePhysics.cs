using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePhysics : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CollectableObj"))
        {
            CollectableObj _obj = other.GetComponent<CollectableObj>();
            if(!_obj.moving)
            {
                Rigidbody _boxRb = other.GetComponent<Rigidbody>();
                if (_boxRb.isKinematic)
                    _boxRb.isKinematic = false;
            }
        }
    }
}
