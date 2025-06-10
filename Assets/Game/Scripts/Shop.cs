using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerStats _player;
    [SerializeField] private Button _button;
    [SerializeField] private ItemView _image;
    [SerializeField] private Score _score;
    [SerializeField] private List<PurchasableItem> _upgrades;

    private int _upgradeCount = 0;

    private void Start()
    {
        _image.SetView(_upgrades[_upgradeCount]);
        _button.interactable = !_upgrades[_upgradeCount].IsBuyed;
    }

    public void NextUpgrade()
    {
        _upgradeCount++;

        if (_upgradeCount == _upgrades.Count)
        {
            _upgradeCount = 0;
        }

        _button.interactable = !_upgrades[_upgradeCount].IsBuyed;
        _image.SetView(_upgrades[_upgradeCount]);
    }

    public void PreviousUpgrade()
    {
        _upgradeCount--;

        if (_upgradeCount < 0) 
        {
            _upgradeCount = _upgrades.Count - 1;
        }

        _button.interactable = !_upgrades[_upgradeCount].IsBuyed;
        _image.SetView(_upgrades[_upgradeCount]);
    }

    public void BuyUpgrade()
    {
        ResourceName[] resources = _upgrades[_upgradeCount].GetResources();
        int count = resources.Length;

        foreach (ResourceName resource in resources) 
        {
            if (_inventory.GetResourceCount(resource) >= _upgrades[_upgradeCount].GetAmountResource(resource))
            {
                count--;
            }
        }

        if(count <= 0) 
        {
            if (_score.CheckScore(_upgrades[_upgradeCount].ScoreNeed))
            {
                _score.DecreaseScore(_upgrades[_upgradeCount].ScoreNeed);

                foreach (ResourceName resource in resources)
                {
                    _inventory.DecreaseResources(resource, _upgrades[_upgradeCount].GetAmountResource(resource));
                }

                Stats[] stats = _upgrades[_upgradeCount].GetStats();

                foreach(Stats stat in stats) 
                {
                    _player.IncreaseStat(stat, (int)_upgrades[_upgradeCount].GetAmountStat(stat));
                }

                _upgrades[_upgradeCount].Buy();
                _button.interactable = !_upgrades[_upgradeCount].IsBuyed;
            }
        }
    }
}
