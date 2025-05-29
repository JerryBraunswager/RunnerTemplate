using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceName _resources;
    [SerializeField] private float _spawnChance;

    public float SpawnChance => _spawnChance;
    public ResourceName Resources => _resources;

    private void Start()
    {
        transform.DOMove(new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, 1f), 0.5f)
            .OnComplete(() => transform.DOScale(0, 0.1f)
            .OnComplete(() => Destroy(gameObject)));
    }
}
