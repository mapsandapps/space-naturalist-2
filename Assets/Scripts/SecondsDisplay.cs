using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondsDisplay : MonoBehaviour
{
  Text secondsText;

  private float startSeconds = 300f;
  private float timer;
  private bool counting;

  // Start is called before the first frame update
  void Start()
  {
    secondsText = GetComponent<Text>();
  }

  // Update is called once per frame
  void Update()
  {
    if (timer <= 0f && counting)
    {
      HitZero();
    }
    else if (timer >= 0f && counting)
    {
      timer -= Time.deltaTime;
      secondsText.text = timer.ToString("F0");
    }
  }

  private void HitZero()
  {
    counting = false;
    FindObjectOfType<LevelLoading>().LoadWinScene();
    // secondsText.text = "0";
    timer = 0.0f;
  }

  public void ResetTimer()
  {
    timer = startSeconds;
    counting = true;
  }

  public void StopTimer()
  {
    // TODO: stop it
    counting = false;
    ResetTimer();
  }
}
