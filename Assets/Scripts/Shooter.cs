using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();
        if (firePoint.localPosition.x > 0)
        {
            bulletRB.velocity = new Vector2(bulletSpeed * 1, bulletRB.velocity.y);
        }
        else
        {
            bulletRB.velocity = new Vector2(bulletSpeed * -1, bulletRB.velocity.y);
        }
    }
}
