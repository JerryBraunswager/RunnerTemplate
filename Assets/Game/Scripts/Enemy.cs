using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent (typeof(ResourceSpawner))]
public class Enemy : Disposable<Enemy>
{
    [SerializeField] private float _shiverAmount;

    private float _speed;
    private float _damage;
    private float _maxHealth;
    private bool _dead = false;
    private Health _currentHealth;
    private ResourceSpawner _spawner;

    public event Action<Resource> CreatedResource;

    private void Awake()
    {
        _currentHealth = GetComponent<Health>();
        _spawner = GetComponent<ResourceSpawner>();
    }

    private void OnEnable()
    {
        _spawner.OnSpawnResource += CreateResource;
    }

    private void OnDisable()
    {
        _spawner.OnSpawnResource -= CreateResource;
    }

    private void Update()
    {
        transform.Translate(-transform.up * (_speed * Time.deltaTime));

        if (_currentHealth.CurrentHealth <= 0 & _dead != true)
        {
            _dead = true;
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerStats stats)) 
        {
            stats.TakeDamage(_damage);
            Invoke(this, true);
        }
    }

    public void Init(float minHealth, float maxHealth, float speed, float damage, int amount)
    {
        _speed = speed;
        _damage = damage;
        _dead = false;
        _maxHealth = Convert.ToInt32( UnityEngine.Random.Range(minHealth, maxHealth));
        _currentHealth.SetHealth(_maxHealth);
        _spawner.SetAmount(amount);
        _spawner.CalculateAmount(_currentHealth.CurrentHealth, minHealth, maxHealth);
    }

    public void PlayAnimation()
    {
        Sequence shiver = DOTween.Sequence();
        float x = transform.position.x;
        shiver.Append(transform.DOMoveX(x - _shiverAmount, 0.1f));
        shiver.Append(transform.DOMoveX(x + _shiverAmount, 0.1f));
        shiver.Append(transform.DOMoveX(x, 0.1f));
        shiver.Play();
    }

    public void GetDamage(float damage)
    {
        _currentHealth.ChangeHealth(damage);
    }

    public void SetAlive()
    {
        _dead = false;
    }

    private void Die()
    {
        _spawner.SpawnResources();
        transform.DOScale(0, 0.3f).OnComplete(() => Invoke(this, true));
    }

    private void CreateResource(Resource obj)
    {
        CreatedResource?.Invoke(obj);
    }
}
