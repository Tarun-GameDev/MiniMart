using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playingMenu;
    [SerializeField] GameObject levelFailedMenu;
    [SerializeField] GameObject levelCompletedMenu;
    [SerializeField] GameObject pauseMenu;
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

    public void LevelFailed()
    {
        playingMenu.SetActive(false);
        levelFailedMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelCompleted()
    {
        StartCoroutine(LevelCompleteActive());
    }

    IEnumerator LevelCompleteActive()
    {
        yield return new WaitForSeconds(2f);
        levelCompletedMenu.SetActive(true);
        playingMenu.SetActive(false);
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
