using System;
using UnityEngine;

public class LevelManager : MonoBehaviour

{
    GameObject level;

   void Start()
   {
    CoreGameSignals.Instance.onLevelInitialized += OnLevelInitialized;
    CoreGameSignals.Instance.onLevelRestart += OnLevelRestart;

    CoreGameSignals.Instance.onPlayerUpgrade += OnPlayerUpgrade;
  
    
   }

    private void OnPlayerUpgrade(float value1, float value2)
    {
       Debug.Log(value1);
       Debug.Log(value2);
    }

    private void OnLevelRestart()
    {
        Debug.Log($"Scene res");
        Destroy(level);
        
    }

    private void OnLevelInitialized(byte _levelIndex)
    {
         Debug.Log($"Level load");
         level = Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {_levelIndex}"));
    }

   
  
}
