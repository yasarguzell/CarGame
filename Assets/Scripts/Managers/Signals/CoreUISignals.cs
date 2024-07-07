using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreUISignals : MonoBehaviour
{
   public static CoreUISignals Instance { get; set; }




public UnityAction onGameStartPanel=delegate{};
public UnityAction onGamePausePanel=delegate{};
public UnityAction onLevelFailedPanel=delegate{};
public UnityAction onLevelSuccesfullPanel=delegate{};
public UnityAction onLevelRestartPanel=delegate{};








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
