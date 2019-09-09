using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour
{
  [SerializeField] float delayInSeconds = 2f;

  public void LoadStartMenu()
  {
    SceneManager.LoadScene("StartMenu");
  }

  public void LoadGameScene()
  {
    StartCoroutine(WaitAndLoad());
    FindObjectOfType<SecondsDisplay>().StopTimer();
    FindObjectOfType<GameSession>().ResetGame();
  }

  IEnumerator WaitAndLoad()
  {
    yield return new WaitForSeconds(delayInSeconds);

    SceneManager.LoadScene("GameScene");
  }

  public void LoadGameOver()
  {
    SceneManager.LoadScene("GameOver");
  }

  public void LoadWinScene()
  {
    SceneManager.LoadScene("WinScene");
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
