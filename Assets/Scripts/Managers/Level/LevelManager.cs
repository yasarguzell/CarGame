using System;
using UnityEngine;

public class LevelManager : MonoBehaviour

{
    GameObject level;

  
    void OnEnable()
    {
        CoreGameSignals.Instance.onLevelInitialized += OnLevelInitialized;
        CoreGameSignals.Instance.onLevelRestart += OnLevelRestart;
        CoreGameSignals.Instance.onGamePause += OnGamePause;
        CoreGameSignals.Instance.onGameResume += OnGameResume;

        

    }


    void OnDisable()
    {
        CoreGameSignals.Instance.onLevelInitialized -= OnLevelInitialized;
        CoreGameSignals.Instance.onLevelRestart -= OnLevelRestart;
        CoreGameSignals.Instance.onGamePause -= OnGamePause;
        CoreGameSignals.Instance.onGameResume -= OnGameResume;

       
    }

    private void OnGameResume()
    {
        Time.timeScale = 1;
        foreach (AudioSource audioSource in allSources)
        {
            audioSource.Play();
        }
    }
    private void OnGamePause()
    {
        Time.timeScale = 0;
        StopAllSounds();
    }

    private AudioSource[] allSources;

    void StopAllSounds()
    {
        // Scene içindeki tüm AudioSource bileşenlerini bul
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        allSources = allAudioSources;

        // Her bir AudioSource'u durdur
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
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
