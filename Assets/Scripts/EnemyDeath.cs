using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Health health;
    [Header("Colliders to enable")]
    [SerializeField] private List<Collider2D> colliders;

    void Awake()
    {
        health.Death += Health_Death;
    }

    private void Health_Death(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
    }
}
