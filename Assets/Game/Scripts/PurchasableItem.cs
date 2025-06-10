using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades")]
public class PurchasableItem : ScriptableObject
{
    [SerializeField] private string _nameRu;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _scoreNeed;
    [SerializeField] private List<PurchaseNeed> _resources;
    [SerializeField] private List<Stat> _stats;
    [SerializeField] private bool _isBuyed = false;

    public string NameRu => _nameRu;
    public Sprite Sprite => _sprite;
    public float ScoreNeed => _scoreNeed;
    public bool IsBuyed => _isBuyed;

    public void Buy()
    {
        _isBuyed = true;
    }

    public ResourceName[] GetResources()
    {
        ResourceName[] resources = new ResourceName[_resources.Count];

        for(int i = 0; i < _resources.Count; i++)
        {
            resources[i] = _resources[i].ResourceName;
        }

        return resources;
    }

    public Stats[] GetStats()
    {
        Stats[] stats = new Stats[_stats.Count];

        for (int i = 0; i < _stats.Count; i++)
        {
            stats[i] = _stats[i].NameEn;
        }

        return stats;
    }

    public string GetName(int index)
    {
        return _stats[index].NameRu;
    }

    public int GetAmountResource(ResourceName resourceName)
    {
        foreach(PurchaseNeed resource in _resources) 
        {
            if (resource.ResourceName == resourceName)
            {
                return resource.Amount;
            }
        }
        return 0;
    }

    public float GetAmountStat(Stats statName)
    {
        foreach (Stat stat in _stats)
        {
            if (stat.NameEn == statName)
            {
                return stat.Amount;
            }
        }
        return 0;
    }
}

[Serializable]
public class PurchaseNeed
{
    [SerializeField] private ResourceName _resourceName;
    [SerializeField] private int _amount;

    public ResourceName ResourceName => _resourceName;
    public int Amount => _amount;
}

[Serializable]
public class PurchaseStats
{
    [SerializeField] private Stats _statName;
    [SerializeField] private int _amount;

    public Stats StatName => _statName;
    public int Amount => _amount;
}
