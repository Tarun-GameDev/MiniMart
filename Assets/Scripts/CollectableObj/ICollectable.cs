using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    void PickUp(GameObject _targetObj);

    void Drop(GameObject _targetObj);
}
