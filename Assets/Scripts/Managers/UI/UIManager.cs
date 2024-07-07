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
    CoreGameSignals.Instance.onGameStart += OnGameStart;
    CoreGameSignals.Instance.onGamePause += OnGamePause;
    CoreGameSignals.Instance.onLevelRestart += OnLevelRestart;
    CoreGameSignals.Instance.onLevelSuccesfull += OnLevelSuccesfull;
    CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;

    CoreUISignals.Instance.onGameStartPanel += OnGameStart;

}

    private void OnLevelFailed()
    {
        throw new NotImplementedException();
    }

    private void OnLevelSuccesfull()
    {
        throw new NotImplementedException();
    }

    private void OnLevelRestart()
    {
        throw new NotImplementedException();
    }

    private void OnGamePause()
    {
        throw new NotImplementedException();
    }

    public void OnGameStart()
    {
        Debug.Log($"Game Start");
        mainPanel.SetActive(false);
    }

    void UnSubscribe()
{
    CoreGameSignals.Instance.onGameStart -= OnGameStart;
    CoreGameSignals.Instance.onGamePause -= OnGamePause;
    CoreGameSignals.Instance.onLevelRestart -= OnLevelRestart;
    CoreGameSignals.Instance.onLevelSuccesfull -= OnLevelSuccesfull;
    CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
}


}
