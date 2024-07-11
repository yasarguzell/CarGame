using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] TMP_Text scoreText;
    byte currentLevel = 0;
    float initialScore=0;
   
    void OnEnable()
    {
        Subscribe();
    }

    void OnDisable()
    {
         UnSubscribe();
    }


    void Subscribe()
    {
        CoreGameSignals.Instance.onLevelFailed += LevelFailed;
        CoreGameSignals.Instance.onLevelRestart += LevelRestart;


        CoreUISignals.Instance.onStartPanel += onStartPanel;
        CoreUISignals.Instance.onPausePanel += onPausePanel;
        CoreUISignals.Instance.onLevelFailedPanel += onLevelFailedPanel;
        CoreUISignals.Instance.onUpgradePanel += onUpgradePanel;
        CoreUISignals.Instance.onGameScoreUpdate += onGameScoreUpdate;
    }

    private void onGameScoreUpdate(float value)
    {
        initialScore = value;
        scoreText.text = "Score: " + initialScore.ToString();
    }

    private void onUpgradePanel()
    {
        upgradePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
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

        CoreUISignals.Instance.onGameScoreUpdate?.Invoke(0);
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
        DataManager.Instance.ResetPlayerData();
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
        CoreGameSignals.Instance.onPlayerUpgrade?.Invoke(15); // player data upgrade HP
        upgradePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameUpgradeTwo()
    {
       CoreGameSignals.Instance.onPlayerUpgradeSpeed?.Invoke(5f); // player data upgrade maxSpeed
        upgradePanel.SetActive(false);
         Time.timeScale = 1;
    }

    public void GameUpgradeThree()
    {
       // CoreGameSignals.Instance.onPlayerUpgrade?.Invoke(1.5f, 1.5f); // player data upgrade 
        upgradePanel.SetActive(false);
         Time.timeScale = 1;
    }


    void UnSubscribe()
    {

        CoreGameSignals.Instance.onLevelFailed -= LevelFailed;
        CoreGameSignals.Instance.onLevelRestart -= LevelRestart;


        CoreUISignals.Instance.onStartPanel -= onStartPanel;
        CoreUISignals.Instance.onPausePanel -= onPausePanel;
        CoreUISignals.Instance.onLevelFailedPanel -= onLevelFailedPanel;
        CoreUISignals.Instance.onUpgradePanel -= onUpgradePanel;
        CoreUISignals.Instance.onGameScoreUpdate -= onGameScoreUpdate;
    }




}
