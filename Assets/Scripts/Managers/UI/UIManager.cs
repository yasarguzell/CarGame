using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<Image> hpImages = new List<Image>();

    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] TMP_Text kmText;
    [SerializeField] TMP_Text fuelText;
    byte currentLevel = 0;
    float _kmInitialScore = 0;
    float _fuelInitialScore = 0;

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
        CoreUISignals.Instance.onGameScoreTextUpdate += onGameScoreUpdate;
        CoreUISignals.Instance.onGameFuelPanelUpdate += onGameFuelPanelUpdate;
        CoreUISignals.Instance.onGameSetHpBarUpdate += onGameSetHpBarUpdate;
        CoreUISignals.Instance.onGameSetHpBarRestartUpdate += onGameSetHpBarRestartUpdate;

    }

    private void onGameSetHpBarRestartUpdate(byte stageValue)
    {
        foreach (var image in hpImages)
        {
            image.DOColor(Color.red, 0.5f);
        }
    }

    private void onGameSetHpBarUpdate(byte stageValue)
    {

        hpImages[stageValue].DOColor(Color.white, 0.5f);
    }
    private void onGameFuelPanelUpdate(float value)
    {
        _fuelInitialScore = value;
        fuelText.text = "Fuel: " + _fuelInitialScore.ToString();
    }


    private void onGameScoreUpdate(float value)
    {
        _kmInitialScore = value;
        kmText.text = "Score: " + _kmInitialScore.ToString();
    }

    private void onUpgradePanel()
    {
        CoreGameSignals.Instance.onGamePause?.Invoke();
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
        CoreGameSignals.Instance.onGamePause?.Invoke();
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
        CoreGameSignals.Instance.onGameResume?.Invoke();
        pausePanel.gameObject.SetActive(false);
    }

    public void GameRestart()
    {
        CoreGameSignals.Instance.onLevelRestart?.Invoke();
        CoreGameSignals.Instance.onLevelInitialized?.Invoke(currentLevel);
        CoreGameSignals.Instance.onGameResume?.Invoke();
        CoreUISignals.Instance.onGameSetHpBarRestartUpdate?.Invoke(2);
        DataManager.Instance.ResetPlayerData();

        pausePanel.gameObject.SetActive(false);
        failPanel.SetActive(false);
    }


    public void GameUpgrade()
    {
        CoreUISignals.Instance.onUpgradePanel?.Invoke();
        CoreGameSignals.Instance.onGamePause?.Invoke();
    }

    public void GameUpgradeOne()
    {
        CoreGameSignals.Instance.onPlayerUpgradeHp?.Invoke(15); // player data upgrade HP
        CoreGameSignals.Instance.onGameResume?.Invoke();

        upgradePanel.SetActive(false);

    }
    public void GameUpgradeTwo()
    {
        CoreGameSignals.Instance.onPlayerUpgradeSpeed?.Invoke(5f); // player data upgrade maxSpeed
        CoreGameSignals.Instance.onGameResume?.Invoke();

        upgradePanel.SetActive(false);

    }

    public void GameUpgradeThree()
    {
        CoreGameSignals.Instance.onPlayerUpgradeWeapon?.Invoke(1.5f); // player data upgrade 
        CoreGameSignals.Instance.onGameResume?.Invoke();

        upgradePanel.SetActive(false);

    }
    public void GameQuit()
    {
        Application.Quit();
    }


    void UnSubscribe()
    {

        CoreGameSignals.Instance.onLevelFailed -= LevelFailed;
        CoreGameSignals.Instance.onLevelRestart -= LevelRestart;


        CoreUISignals.Instance.onStartPanel -= onStartPanel;
        CoreUISignals.Instance.onPausePanel -= onPausePanel;
        CoreUISignals.Instance.onLevelFailedPanel -= onLevelFailedPanel;
        CoreUISignals.Instance.onUpgradePanel -= onUpgradePanel;
        CoreUISignals.Instance.onGameScoreTextUpdate -= onGameScoreUpdate;
        CoreUISignals.Instance.onGameFuelPanelUpdate -= onGameFuelPanelUpdate;
    }

}
