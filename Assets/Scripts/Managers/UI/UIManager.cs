using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  
byte currentLevel=0;
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
   CoreGameSignals.Instance.onLevelFailed += LevelFailed;
   CoreGameSignals.Instance.onLevelSuccesfull += LevelSuccesfull;
   CoreGameSignals.Instance.onLevelRestart += LevelRestart;
}

    private void LevelRestart()
   {
    CoreUISignals.Instance.onLevelRestartPanel?.Invoke();
    }

    private void LevelSuccesfull()
    {
        CoreUISignals.Instance.onLevelSuccesfullPanel?.Invoke();
       
    }

    private void LevelFailed()
    {
        CoreUISignals.Instance.onLevelFailedPanel?.Invoke();
    }

    public void GameStart()
{
    CoreGameSignals.Instance.onLevelInitialized?.Invoke(currentLevel);
    CoreUISignals.Instance.onStartPanel?.Invoke(); // close panel

}

public void GamePause()
{
    CoreGameSignals.Instance.onGamePause?.Invoke();
    CoreUISignals.Instance.onPausePanel?.Invoke();
}



void UnSubscribe()
{
  
   CoreGameSignals.Instance.onLevelFailed -= LevelFailed;
   CoreGameSignals.Instance.onLevelSuccesfull -= LevelSuccesfull;
   CoreGameSignals.Instance.onLevelRestart -= LevelRestart;
}




}
