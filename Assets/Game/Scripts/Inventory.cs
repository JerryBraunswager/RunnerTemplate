using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private int[] _items = new int[9];

    private void Start()
    {
        for(int i = 0; i < ((int)ResourceName.Obsidian);  i++) 
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
        YandexGame.SaveProgress();
    }
}
