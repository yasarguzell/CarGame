using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] GameObject mainPanel;
   [SerializeField] GameObject pausePanel;
   [SerializeField] GameObject levelFailedPanel;
   [SerializeField] GameObject levelSuccesfullPanel;
   

void Start()
{
    Subscribe();
}
void OnEnable()
{
    //Subscribe();
}

void OnDisable()
{
   // UnSubscribe();
}






void Subscribe()
{
    

    CoreUISignals.Instance.onGameStartPanel += OnGameStart;
    CoreUISignals.Instance.onGamePausePanel += OnGamePause;
    CoreUISignals.Instance.onLevelFailedPanel += OnLevelFailed;
    CoreUISignals.Instance.onLevelSuccesfullPanel += OnLevelSuccesfull;
    CoreUISignals.Instance.onLevelRestartPanel += OnLevelRestart;

}

    private void OnLevelRestart()
    {
        throw new NotImplementedException();
    }

    private void OnLevelSuccesfull()
    {
        throw new NotImplementedException();
    }

    private void OnLevelFailed()
    {
        throw new NotImplementedException();
    }

    private void OnGamePause()
    {
        Debug.Log($"Game Pause");
    }

    private void OnGameStart()
    {
        Debug.Log($"Game Start");
        mainPanel.SetActive(false);
    }

  



void UnSubscribe()
{
    CoreUISignals.Instance.onGameStartPanel -= OnGameStart;
    CoreUISignals.Instance.onGamePausePanel -= OnGamePause;
    CoreUISignals.Instance.onLevelFailedPanel -= OnLevelFailed;
    CoreUISignals.Instance.onLevelSuccesfullPanel -= OnLevelSuccesfull;
    CoreUISignals.Instance.onLevelRestartPanel -= OnLevelRestart;
}


}
