using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject upgradePanel;
    byte currentLevel = 0;
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
        CoreGameSignals.Instance.onLevelRestart += LevelRestart;
        

        CoreUISignals.Instance.onStartPanel += onStartPanel;
        CoreUISignals.Instance.onPausePanel += onPausePanel;
        CoreUISignals.Instance.onLevelFailedPanel += onLevelFailedPanel;
        CoreUISignals.Instance.onUpgradePanel += onUpgradePanel;
    }



    private void onUpgradePanel()
    {
       upgradePanel.gameObject.SetActive(true);
    }

    private void onLevelFailedPanel()
    {
        failPanel.gameObject.SetActive(true);
    }

    private void onPausePanel()
    {
        pausePanel.gameObject.SetActive(true);
    }

    private void onStartPanel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
     
    }

    private void LevelRestart()
    {
        CoreUISignals.Instance.onLevelRestartPanel?.Invoke();
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

    public void GameResume()
    {
       pausePanel.gameObject.SetActive(false);
    }

    public void GameRestart()
    {
        CoreGameSignals.Instance.onLevelRestart?.Invoke();
        CoreGameSignals.Instance.onLevelInitialized?.Invoke(currentLevel);

        pausePanel.gameObject.SetActive(false);
        failPanel.SetActive(false);
    }
    

    public void GameFail()
    {
        CoreGameSignals.Instance.onLevelFailed?.Invoke();
    }

    public void GameUpgrade()
    {
      
       CoreUISignals.Instance.onUpgradePanel?.Invoke();

    }

    public void GameUpgradeOne()
    {
         CoreGameSignals.Instance.onPlayerUpgrade?.Invoke(1.5f, 1.5f); // player data upgrade 
         upgradePanel.SetActive(false);
    }
    public void GameUpgradeTwo()
    {
         CoreGameSignals.Instance.onPlayerUpgrade?.Invoke(1.5f, 1.5f); // player data upgrade 
          upgradePanel.SetActive(false);
    }

    public void GameUpgradeThree()
    {
         CoreGameSignals.Instance.onPlayerUpgrade?.Invoke(1.5f, 1.5f); // player data upgrade 
          upgradePanel.SetActive(false);
    }


    void UnSubscribe()
    {

        CoreGameSignals.Instance.onLevelFailed -= LevelFailed;
        CoreGameSignals.Instance.onLevelRestart -= LevelRestart;
    }




}
