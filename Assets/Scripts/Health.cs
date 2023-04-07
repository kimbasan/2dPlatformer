using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Start value")]
    [SerializeField] private float maxHealth;
    [Header("Game values")]
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isAlive;
    [Header("View")]
    [SerializeField] private SpriteRenderer sprite;

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
            if (HealthChanged != null)
            {
                HealthChanged(this, EventArgs.Empty);
            }
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
