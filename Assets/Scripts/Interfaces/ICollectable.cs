using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    void PickUp(GameObject _parentTran, Vector3 _targetPos, Storage _newStorage);

}
