using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _prefab;
    private Queue<Transform> _pool = new Queue<Transform>();

    public Transform Get()
    {
        if (_pool.Count > 0)
        {
            Transform obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            return Instantiate(_prefab);
        }
    }

    public void ReturnToPool(Transform obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
