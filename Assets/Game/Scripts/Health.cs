using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _currentHealth;

    public event Action<float> HealthChanged;

    public float CurrentHealth => _currentHealth;

    public void SetHealth(float health)
    {
        _currentHealth = health;
        HealthChanged?.Invoke(_currentHealth);
    }

    public void ChangeHealth(float amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _currentHealth -= amount;
        HealthChanged?.Invoke(_currentHealth);
    }
}
