using UnityEngine;
using UnityEngine.Events;

public class CoreGameSignals : MonoBehaviour
{

#region  Singleton
public static CoreGameSignals Instance { get; set; }


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
#endregion



public UnityAction<byte> onLevelInitialized=delegate{};
//public UnityAction onGameStart=delegate{};
public UnityAction onGamePause=delegate{};
public UnityAction onLevelRestart=delegate{};
public UnityAction onLevelFailed=delegate{};
public UnityAction<float,float> onPlayerUpgrade=delegate{};






}
