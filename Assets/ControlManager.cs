using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager instance;
    public GameObject cashPrefab;
    public Transform startPosTrans;
    public List<Cash> cashList;
    bool playerIn;

    public int numXCells = 5; // Number of cells in the X-axis.
    public int numYCells = 5; // Number of cells in the Y-axis.
    public int numZCells = 5; // Number of cells in the Z-axis.
    public Vector3 cellSizeVector = Vector3.one;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnCash();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = true;
            SendCash();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = false;
        }
    }

    public void spawnCash()
    {
        int newIndex = cashList.Count;
        int x = newIndex % numXCells;
        int y = (newIndex / numXCells) % numYCells;
        int z = newIndex / (numXCells * numYCells);

        Vector3 _position = new Vector3(x * cellSizeVector.x,z * cellSizeVector.z, y * cellSizeVector.y);
        Cash _cash = ObjectPoolManager.SpawnGameObject(cashPrefab, _position + startPosTrans.position, Quaternion.identity).GetComponent<Cash>();

        cashList.Add(_cash);
    }

    public void SendCash()
    {
        if(cashList.Count > 0)
        {
            Cash _cash = cashList[cashList.Count - 1];
            _cash.StartMoving();
            cashList.Remove(_cash);
            StartCoroutine(SendCashAgain());
        }    
    }

    IEnumerator SendCashAgain()
    {
        yield return new WaitForSeconds(.03f);
        if (playerIn && cashList.Count > 0)
            SendCash();
    }
}
