using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour
{
    
    Transform player; // Reference to the player's transform
    public float moveSpeed = 3.0f; // Adjust the speed as needed
    public bool canMove = false;


    private void Start()
    {
        if (player == null)
            player = LevelManager.instance.player.transform;
    }

    private void Update()
    {
        if (canMove && player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LevelManager.instance.AddMoney(1);
            canMove = false;
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    private void OnEnable()
    {
        canMove = false;
    }
}
