using DG.Tweening;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerStats))]
public class Shoot : MonoBehaviour
{
    [SerializeField] private BulletPool _pool;

    private PlayerStats _playerStats;
    private bool _isShoot = false;
    private float _time;
    private Vector3 _scale;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _time = _playerStats.GetStat(Stats.AttackSpeed);
        _scale = transform.localScale;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _pool.PooledObjects.Count(); i++)
        {
            _pool.PooledObjects.ElementAt(i).Disposed -= ReturnResourceInPool;
        }
    }

    private void Update()
    {
        if(_playerStats.IsGamePlay == false)
        {
            return;
        }

        if (_isShoot)
        {
            _time -= Time.deltaTime;

            if(_time <= 0)
            {
                float newScale = _scale.x + 0.1f;
                transform.DOScale(new Vector3(newScale, newScale, _scale.z), 0.1f)
                    .OnComplete(() => transform.DOScale(_scale, 0.1f)
                    .OnComplete(() => SpawnBullet()));
                _time = _playerStats.GetStat(Stats.AttackSpeed);
            }
        }
    }

    public void OnClick(CallbackContext context)
    {
        if(context.performed)
        {
            _isShoot = true;
        }

        if(context.canceled)
        {
            _isShoot = false;
        }
    }

    private void SpawnBullet()
    {
        Bullet bullet = _pool.GetObject();
        bullet.Init(_playerStats.GetStat(Stats.BulletSpeed), _playerStats.GetStat(Stats.Damage));
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        bullet.transform.gameObject.SetActive(true);
        bullet.Disposed += ReturnResourceInPool;
    }

    private void ReturnResourceInPool(Bullet resource, bool result)
    {
        _pool.PutObject(resource);
        resource.Disposed -= ReturnResourceInPool;
    }
}
