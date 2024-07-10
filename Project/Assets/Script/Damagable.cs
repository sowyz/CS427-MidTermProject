using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] 
    private int currentHealth;
    public UnityEvent OnDeath;
    public UnityEvent<float> OnHealthChanged;
    public UnityEvent OnHit, OnHeal;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set 
        { 
            currentHealth = value;
            OnHealthChanged?.Invoke((float)currentHealth / maxHealth); 
        }
    }
    private void Start()
    {
        CurrentHealth = maxHealth;
    }  

    internal void Hit(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    } 

    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);
        OnHeal?.Invoke();
    }
}
