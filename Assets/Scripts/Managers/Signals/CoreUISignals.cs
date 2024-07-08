using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreUISignals : MonoBehaviour
{
   public static CoreUISignals Instance { get; set; }




public UnityAction onStartPanel=delegate{};
public UnityAction onPausePanel=delegate{};
public UnityAction onLevelFailedPanel=delegate{};
public UnityAction onLevelSuccesfullPanel=delegate{};
public UnityAction onLevelRestartPanel=delegate{};

public UnityAction onUpdateTree=delegate{};
public UnityAction onScoreAdded=delegate{};
public UnityAction onScoreReset=delegate{};
public UnityAction onScoreRemoved=delegate{};









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
