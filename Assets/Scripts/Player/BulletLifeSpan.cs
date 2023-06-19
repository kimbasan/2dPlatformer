using UnityEngine;

public class BulletLifeSpan : MonoBehaviour
{
    [SerializeField] private float lifetime;
    private float timer;

    private void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime) 
        { 
            gameObject.SetActive(false);
        }
    }
}
