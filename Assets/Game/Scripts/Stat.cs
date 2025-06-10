using System;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private Stats _stat;
    [SerializeField] private string _nameRu;
    [SerializeField] private float _amount;
    [SerializeField] private float _increaseAmount;

    public Stats NameEn =>_stat;
    public string NameRu => _nameRu;
    public float Amount => _amount;
    public float IncreaseAmount => _increaseAmount;

    public void SetStat(Stats stat, string nameRu, float amount, float increaseAmount)
    {
        _stat = stat;
        _nameRu = nameRu;
        _amount = amount;
        _increaseAmount = increaseAmount;
    }

    public void IncreaseStat(int amount)
    {
        if(amount <= 0)
        {
            return;
        }

        _amount += (_increaseAmount * amount);
    }
}
