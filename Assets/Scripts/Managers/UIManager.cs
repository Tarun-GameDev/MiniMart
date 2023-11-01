using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playingMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject upgradesMenu;
    [SerializeField] TextMeshProUGUI cashText;
    public static bool gamePaused = false;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playingMenu.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        playingMenu.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void UpgradesButton()
    {
        playingMenu.SetActive(false);
        upgradesMenu.SetActive(true);

    }

    public void UpgradesCancelButton()
    {
        playingMenu.SetActive(true);
        upgradesMenu.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetCashText(int cashAmount)
    {
        cashText.text = cashAmount.ToString();
    }
}
