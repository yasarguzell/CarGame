using UnityEngine;
using UnityEngine.Events;

public class CoreUISignals : MonoBehaviour
{
public static CoreUISignals Instance { get; set; }



public UnityAction onStartPanel=delegate{};
public UnityAction onPausePanel=delegate{};
public UnityAction onLevelFailedPanel=delegate{};
public UnityAction onLevelRestartPanel=delegate{};
public UnityAction onUpgradePanel=delegate{};

public UnityAction<float> onGameScoreUpdate=delegate{};




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










}
