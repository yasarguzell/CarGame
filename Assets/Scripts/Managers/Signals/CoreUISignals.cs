using UnityEngine;
using UnityEngine.Events;

public class CoreUISignals : MonoBehaviour
{
public static CoreUISignals Instance { get; set; }



public UnityAction onStartPanel=delegate{};
public UnityAction onPausePanel=delegate{};
public UnityAction onLevelFailedPanel=delegate{};
public UnityAction onLevelRestartPanel=delegate{};


public UnityAction<byte> onGameSetHpBarUpdate=delegate{};
public UnityAction<byte> onGameSetHpBarRestartUpdate=delegate{};
public UnityAction<float> onGameScoreTextUpdate=delegate{};
public UnityAction<float> onGameFuelTextUpdate=delegate{};




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
