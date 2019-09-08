using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour
{
  public void LoadStartMenu()
  {
    SceneManager.LoadScene("StartMenu");
  }

  public void LoadGameScene()
  {
    // TODO: block breaker has info about how to do levels
    SceneManager.LoadScene("GameScene");
  }

  public void LoadGameOver()
  {
    SceneManager.LoadScene("GameOver");
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
