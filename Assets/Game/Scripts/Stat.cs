using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private Stats _stat;
    [SerializeField] private string _nameRu;
    [SerializeField] private float _amount;

    public Stats NameEn =>_stat;
    public string NameRu => _nameRu;
    public float Amount => _amount;

    public void SetStat(Stats stat, string nameRu, float amount)
    {
        _stat = stat;
        _nameRu = nameRu;
        _amount = amount;
    }
}
