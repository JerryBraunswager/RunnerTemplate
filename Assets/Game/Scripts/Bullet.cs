using UnityEngine;

public class Bullet : Disposable<Bullet>
{
    private float _speed;
    private float _damage;

    private void Update()
    {
        transform.Translate(-transform.up * (_speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.PlayAnimation();
            enemy.GetDamage(_damage);
            Invoke(this, true);
        }
    }

    public void Init(float speed, float damage)
    {
        _speed = speed;
        _damage = damage;
    }
}
