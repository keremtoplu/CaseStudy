using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
  [SerializeField]
  private float timer;

  private GameState currentGameState;

  public GameState CurrentGameState => currentGameState;
  public event Action<GameState> GameStateChanged;

    private float timerStartValue;
  void Start()
  {

    timerStartValue = timer;

    if(PlayerPrefs.GetInt("FirstGame")==0)
      GameStateChanged?.Invoke(GameState.Start);
    else
      UpdateGameState(GameState.InGame);

    
  }

  void Update()
  {
    if (currentGameState == GameState.InGame)
    {
      timer -= Time.deltaTime;
      UIManager.Instance.PlayTime.text=((int)timer).ToString();
      if (timer <= 0)
      {
        UpdateGameState(GameState.Fail);
      }
    }
  }

  public void ReloadScene()
  {
    PlayerPrefs.SetInt("FirstGame",1);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void UpdateGameState(GameState newState)
  {
    currentGameState = newState;
    if (currentGameState == GameState.InGame)
      timer = timerStartValue;

    GameStateChanged?.Invoke(newState);
  }


}
