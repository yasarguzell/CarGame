using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance { get; set; }
    public PlayerData playerData;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        playerData = new PlayerData();
        
        playerData.playerHp = 100;
        playerData.playerSpeed = 5;

    }

    void OnEnable()
    {
         Subscribe();
    }

    void OnDisable()
    {
        UnSubscribe();
    }

    public void ResetPlayerData()
    {
        playerData.playerHp = 100;
        playerData.playerSpeed = 5;
    }

    void Subscribe()
    {
        CoreGameSignals.Instance.onPlayerUpgrade += GameUpgrade;
        CoreGameSignals.Instance.onPlayerUpgradeSpeed += onPlayerUpgradeSpeed;
    }

    private void onPlayerUpgradeSpeed(float arg0)
    {
        playerData.playerSpeed += arg0;
        Debug.Log($"speed: {playerData.playerSpeed}");
    }

    private void GameUpgrade(int arg0)
    {
        Debug.Log($"GameUpgrade: {arg0}");
        playerData.playerHp += arg0;
        Debug.Log($"Hp: {playerData.playerHp}");
    }

    void UnSubscribe()
    {
        CoreGameSignals.Instance.onPlayerUpgrade -= GameUpgrade;
        CoreGameSignals.Instance.onPlayerUpgradeSpeed -= onPlayerUpgradeSpeed;
    }


}
