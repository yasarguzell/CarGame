using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour

{

    float levelIndex;
   void Start()
   {
    CoreGameSignals.Instance.onGameStart += OnGameStart;
  
    
   }

    private void OnGameStart()
    {
        Debug.Log($"Level load");
        GameObject level = Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {levelIndex}"));
    }
}
