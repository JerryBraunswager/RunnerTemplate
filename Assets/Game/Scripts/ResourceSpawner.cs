using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private List<Resource> _resources;

    private int _minResources;
    private int _maxResources;
    private int _amountResources;

    public event Action<Resource> OnSpawnResource;

    public void SetAmount(int amount)
    {
        _minResources = 1;
        _maxResources = amount + 1;
    }

    public void CalculateAmount(float health, float minHealth, float maxHealth)
    {
        float bias = minHealth / (maxHealth - minHealth);
        float percent = (health / (maxHealth - minHealth) - bias) * 100;
        _amountResources = Mathf.RoundToInt((_maxResources - _minResources) / 100f * percent);
    }

    public void SpawnResources()
    {
        for(int i = 0; i < _amountResources; i++) 
        {
            float chance = UnityEngine.Random.value;

            foreach(Resource resource in _resources)
            {
                if(chance <= resource.SpawnChance)
                {
                    Resource news = Instantiate(resource);
                    OnSpawnResource?.Invoke(resource);
                    news.transform.position = transform.position;
                    break;
                }
            }
        }
    }
}
