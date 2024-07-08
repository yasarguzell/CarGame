using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour

{

   void Start()
   {
    CoreGameSignals.Instance.onLevelInitialized += OnLevelInitialized;
  
    
   }

    private void OnLevelInitialized(byte _levelIndex)
    {
         Debug.Log($"Level load");
        GameObject level = Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {_levelIndex}"));
    }

   
  
}
