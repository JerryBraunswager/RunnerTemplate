using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(Health))]
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private TimeFlow _timeFlow;
    [SerializeField] private LinesPool _lines;
    [SerializeField] private List<Stat> _startStats = new List<Stat>();

    private RectTransform _line;
    private Health _currentHealth;
    private List<Stat> _stats = new List<Stat>();

    public bool IsGamePlay => _timeFlow.isGamePlay;

    private void Awake()
    {
        _currentHealth = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _lines.LineChanged += SaveLine;
        _currentHealth.HealthChanged += CheckAlive;
    }

    private void OnDisable()
    {
        _lines.LineChanged -= SaveLine;
        _currentHealth.HealthChanged -= CheckAlive;
    }

    private void Update()
    {
        if (_timeFlow.isGamePlay == false || _line == null)
        {
            return;
        }

        Vector3 position = Camera.main.ScreenToWorldPoint(_line.position);
        Vector3 result = new Vector3(position.x, position.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, result, GetStat(Stats.StrafeSpeed) * Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        _currentHealth.ChangeHealth(amount);
    }

    public float GetStat(Stats statName)
    {
        foreach(Stat stat in _stats) 
        {
            if(stat.NameEn == statName)
            {
                return stat.Amount;
            }
        }

        return 0;
    }

    public void IncreaseStat(Stats statName, int increaseAmount)
    {
        foreach(Stat stat in _startStats)
        {
            if (stat.NameEn == statName)
            {
                stat.IncreaseStat(increaseAmount);
            }
        }

        YandexGame.savesData.Stats = _startStats;
        YandexGame.SaveProgress();
    }

    public void NewHeroCreate()
    {
        if(YandexGame.savesData.Stats.Count == _startStats.Count)
        {
            _startStats = YandexGame.savesData.Stats;
        }
        else
        {
            YandexGame.savesData.Stats = _startStats;
            YandexGame.SaveProgress();
        }

        foreach (Stat stat in _startStats)
        {
            if (stat.NameEn == Stats.Health)
            {
                _currentHealth.SetHealth(stat.Amount);
            }

            Stat newStat = new Stat();
            newStat.SetStat(stat.NameEn, stat.NameRu, stat.Amount, stat.IncreaseAmount);
            _stats.Add(newStat);
        }
    }

    private void SaveLine(RectTransform line)
    {
        _line = line;
    }

    private void CheckAlive(float health)
    {
        if(health <= 0)
        {
            _timeFlow.StopGame();
        }
    }
}
