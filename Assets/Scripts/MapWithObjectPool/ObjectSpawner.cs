using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float groundSpawnDistance = 50f;

    public Transform playerTransform;

    public static ObjectSpawner instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //playerTransform = FindAnyObjectByType<CarMovementController>().transform;
    }
    public int level_index = 1;
    public void SpawnGround()
    {

        Vector3 spawnPosition = new Vector3(0, 0, playerTransform.position.z + groundSpawnDistance);
        ObjectPooler.Instance.SpawnFromPool("Road" + level_index, spawnPosition, Quaternion.identity);

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
