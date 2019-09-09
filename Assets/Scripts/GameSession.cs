using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
  int score = 0;

  private void Awake()
  {
    SetUpSingleton();
  }

  private void SetUpSingleton()
  {
    FindObjectOfType<SecondsDisplay>().ResetTimer();
    int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

    if (numberOfGameSessions > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }

  public int GetScore()
  {
    return score;
  }

  public void AddToScore(int score)
  {
    this.score += score;
  }

  public void ResetGame()
  {
    FindObjectOfType<SecondsDisplay>().StopTimer();
    Destroy(gameObject);
  }
}
