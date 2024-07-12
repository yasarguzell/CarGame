using System;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float groundSpawnDistance = 50f;
    public Transform playerTransform;
    public int level_index = 1;
    public static ObjectSpawner instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CoreGameSignals.Instance.onLevelRestart += OnLevelRestart;
        //playerTransform = FindAnyObjectByType<CarMovementController>().transform;
    }

    private void OnLevelRestart()
    {
        level_index = 1;
    }
    
    public void SpawnGround()
    {
        Vector3 spawnPosition = new Vector3(0, 0, playerTransform.position.z + groundSpawnDistance);
        ObjectPooler.Instance.SpawnFromPool("Road" + level_index, spawnPosition, Quaternion.identity);
        spawnPosition += new Vector3(0,0,groundSpawnDistance);

        if (level_index != 5)
        {
            level_index++;
        }
        else
        {
            level_index = 1;
        }
    }
}
