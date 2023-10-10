using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public UIManager uiManager;

    public PlayerController player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();

        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
    }
}
