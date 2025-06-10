using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeteoritPool))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _minEnemyHealth;
    [SerializeField] private float _maxEnemyHealth;
    [SerializeField] private float _positionY;
    [SerializeField] private List<RectTransform> _lines;
    [SerializeField] private float _sleepTime;
    [SerializeField] private TimeFlow _timeFlow;
    [SerializeField] private Score _score;
    [SerializeField] private PlayerStats _player;
    [SerializeField] private HighScore _highScore;

    private MeteoritPool _pool;
    private float _currentSleepTime;
    private float _time;
    private float _currentDamage;
    private float _currentMinEnemyHealth;
    private float _currentMaxEnemyHealth;

    public event Action<Resource> SavedResource;

    private void Awake()
    {
        _pool = GetComponent<MeteoritPool>();
    }

    private void Update()
    {
        if(_timeFlow.isGamePlay == false)
        {
            return;
        }

        _time += Time.deltaTime;

        if (_time >= _currentSleepTime)
        {
            int line = UnityEngine.Random.Range(0, _lines.Count);
            SpawnEnemy(line);
            _time = 0;
        }
        
    }

    private void OnEnable()
    {
        _highScore.ChangedCurrentScore += IncreaseStats;
    }

    private void OnDisable()
    {
        _highScore.ChangedCurrentScore -= IncreaseStats;
    }

    private void IncreaseStats(float score)
    {
        _currentDamage = _damage + score;
        _currentMinEnemyHealth = _minEnemyHealth + score;
        _currentMaxEnemyHealth = _maxEnemyHealth + score;
        _currentSleepTime = _sleepTime - (score * 0.001f);
    }

    private void SpawnEnemy(int x)
    {
        Enemy enemy = _pool.GetObject();
        enemy.Init(_currentMinEnemyHealth, _currentMaxEnemyHealth, _speed, _currentDamage, _player.GetStat(Stats.DropsAmount));
        enemy.CreatedResource += SaveResource;
        Vector3 position = Camera.main.ScreenToWorldPoint(_lines[x].position);
        enemy.transform.position = new Vector3(position.x, transform.position.y, transform.position.z);
        enemy.transform.gameObject.SetActive(true);
        enemy.Disposed += ReturnResourceInPool;
        enemy.Disposed += _score.IncreaseScore;
    }

    private void SaveResource(Resource obj)
    {
        SavedResource?.Invoke(obj);
    }

    private void ReturnResourceInPool(Enemy resource, bool result)
    {
        _pool.PutObject(resource);
        resource.CreatedResource -= SaveResource;
        resource.Disposed -= ReturnResourceInPool;
        resource.Disposed -= _score.IncreaseScore;
    }
}
