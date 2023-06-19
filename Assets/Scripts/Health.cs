using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Start value")]
    [SerializeField] private float maxHealth;

    [Header("View")]
    [SerializeField] private SpriteRenderer sprite;

    [Header("Readonly debug")]
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isAlive;

    public event EventHandler HealthChanged;
    public event EventHandler Death;

    public void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    public float GetFillAmount()
    {
        return currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isAlive)
        {
            currentHealth -= damage;
            ShowDamageTaken();
            TriggerHealthChanged();
            CheckAlive();
        }
    }

    private void CheckAlive()
    {
        isAlive = currentHealth > 0;
        if (!isAlive)
        {
            if (Death != null)
            {
                Death(this, EventArgs.Empty);
            }
        }
    }

    public void AddHealth(float heal)
    {
        currentHealth+= heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        TriggerHealthChanged();
    }

    private void TriggerHealthChanged()
    {
        if (HealthChanged != null)
        {
            HealthChanged(this, EventArgs.Empty);
        }
    }

    private IEnumerator FlashDamage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
    private void ShowDamageTaken()
    {
        StartCoroutine(FlashDamage());
    }
}
