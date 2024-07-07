using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

public static GameManager Instance { get; set; }

void Awake()
{
    if (Instance != null)
    {
        Destroy(this);
    }
    else
    {
        Instance = this;
    }
}
  public void GameStart()
  {
    CoreGameSignals.Instance.onGameStart?.Invoke();
    CoreUISignals.Instance.onGameStartPanel?.Invoke();
  }

  public void GamePause()
  {
    CoreGameSignals.Instance.onGamePause?.Invoke();
    CoreUISignals.Instance.onGamePausePanel?.Invoke();
  }


  public void LevelFailed()
  {
    CoreGameSignals.Instance.onLevelFailed?.Invoke();
    CoreUISignals.Instance.onLevelFailedPanel?.Invoke();
  }

  public void LevelSuccesfull()
  {
    CoreGameSignals.Instance.onLevelSuccesfull?.Invoke();
    CoreUISignals.Instance.onLevelSuccesfullPanel?.Invoke();
  }
}
