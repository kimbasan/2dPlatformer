using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private PointEffector2D explosionEffector;
    [SerializeField] private GameObject fire;
    [SerializeField] private List<GameObject> exposionParticles;
    [SerializeField] private GameObject sprite;
    [SerializeField] private Collider2D barrelCollider;
    [SerializeField] private Rigidbody2D rigidBody;

    public void StartExplosionTimer()
    {
        if (gameObject.activeSelf)
        {
            fire.SetActive(true);
            StartCoroutine(ExplodeAfterWait());
        }
    }

    public void Explode()
    {
        explosionEffector.enabled = true;
        foreach (GameObject particles in exposionParticles)
        {
            particles.SetActive(true);
        }
        StartCoroutine(RemoveAfterWait());
    }

    private IEnumerator ExplodeAfterWait()
    {
        yield return new WaitForSeconds(3);
        Explode();
    }

    private IEnumerator RemoveAfterWait()
    {
        yield return new WaitForSeconds(0.2f);
        explosionEffector.enabled = false;
        fire.SetActive(false);
        sprite.SetActive(false);
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        barrelCollider.enabled = false;
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
