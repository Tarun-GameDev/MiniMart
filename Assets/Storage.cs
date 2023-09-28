using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public Transform[] positions;
    public int StoredObj = 0;

    public Transform SetPositionAt()
    {

        StoredObj++;
        return positions[StoredObj];
    }

    public bool maxCountReached()//check storage is full or not
    {
        if (StoredObj == positions.Length)
            return true;
        else
            return false;
    }

    public void ResetPositions()
    {
        //whenever objects loaded to truck
        StoredObj--;
    }
}
