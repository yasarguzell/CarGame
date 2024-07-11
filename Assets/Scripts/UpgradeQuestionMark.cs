using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeQuestionMark : MonoBehaviour
{
    [SerializeField] private ParticleSystemData[] _particleSystemDatas;
    [SerializeField] private float _particleSizeModifier;

    private UIManager _uiManager;

    /*private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoreUISignals.Instance.onUpgradePanel?.Invoke();
            gameObject.SetActive(false);
        }
    }*/

    private void OnValidate()
    {
        foreach (ParticleSystemData particleData in _particleSystemDatas)
        {
            var newSize = particleData.OriginalStartSize * _particleSizeModifier;
            var main = particleData.ParticleSystem.main;
            main.startSize = new ParticleSystem.MinMaxCurve(newSize.x, newSize.y);
        }
    }

    [Serializable]
    struct ParticleSystemData
    {
        public ParticleSystem ParticleSystem;
        public Vector2 OriginalStartSize;
    }
}
