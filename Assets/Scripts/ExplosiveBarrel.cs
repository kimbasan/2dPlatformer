using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private PointEffector2D explosionEffector;
    [SerializeField] private GameObject fire;
    [SerializeField] private List<GameObject> exposionParticles;

    public void CatchFire()
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
        yield return new WaitForSeconds(0.1f);
        explosionEffector.enabled = false;
        fire.SetActive(false);
        gameObject.SetActive(false);
    }
}
