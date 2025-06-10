using System;
using UnityEngine;
using YG;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private int[] _items = new int[9];

    public event Action ChangedResource;

    private void Start()
    {
        for(int i = 0; i <= ((int)ResourceName.Obsidian);  i++) 
        {
            _items[i] = YandexGame.savesData.Resources[i];
        }
    }

    private void OnEnable()
    {
        _spawner.SavedResource += SaveResource;
    }

    private void OnDisable()
    {
        _spawner.SavedResource -= SaveResource;
    }

    private void SaveResource(Resource obj)
    {
        _items[(int)obj.Resources] += 1;
        YandexGame.savesData.Resources[(int)obj.Resources] = _items[(int)obj.Resources];
        ChangedResource?.Invoke();
        YandexGame.SaveProgress();
    }

    public int GetResourceCount(ResourceName resource)
    {
        return _items[(int)resource];
    }

    public void DecreaseResources(ResourceName resource, int amount)
    {
        if(amount < 0) 
        {
            return;
        }

        _items[(int)resource] -= amount;
        ChangedResource?.Invoke();
    }
}
