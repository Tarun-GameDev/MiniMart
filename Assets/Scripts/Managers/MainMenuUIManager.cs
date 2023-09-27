using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene(PlayerPrefs.GetInt("LevelUnlocked", 0) + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }    
}
