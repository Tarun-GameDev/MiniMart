using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [SerializeField]
    bool physicsOn = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CollectableObj"))
        {
            if (physicsOn)
                ActivatePhysics(other);
            else
                DeactivatePhysics(other);
        }
    }

    void ActivatePhysics(Collider _other)
    {
        Debug.Log("Activating");        
        Rigidbody _boxRb = _other.GetComponent<Rigidbody>();
        if (_boxRb.isKinematic)
        {
            _boxRb.isKinematic = false;
        }

        CollectableObj _obj = _other.GetComponent<CollectableObj>();

        if (_obj != null)
            _obj.moving = false;
    }

    void DeactivatePhysics(Collider _other)
    {
        Debug.Log("Deactivating");
        Rigidbody _boxRb = _other.GetComponent<Rigidbody>();

        if (!_boxRb.isKinematic)
        {
            _boxRb.velocity = Vector3.zero;
            _boxRb.isKinematic = true;

            //resetPos
            //resetRot
            _other.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        CollectableObj _obj = _other.GetComponent<CollectableObj>();

        if (_obj != null)
            _obj.moving = false;
    }
}
