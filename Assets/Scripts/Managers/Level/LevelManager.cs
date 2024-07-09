using System;
using UnityEngine;

public class LevelManager : MonoBehaviour

{
    GameObject level;

  
    void OnEnable()
    {
        CoreGameSignals.Instance.onLevelInitialized += OnLevelInitialized;
        CoreGameSignals.Instance.onLevelRestart += OnLevelRestart;

        

    }

    void OnDisable()
    {
        CoreGameSignals.Instance.onLevelInitialized -= OnLevelInitialized;
        CoreGameSignals.Instance.onLevelRestart -= OnLevelRestart;

       
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
