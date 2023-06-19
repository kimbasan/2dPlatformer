using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerMovement movement;

    [SerializeField] private Image statusImage;

    [SerializeField] private List<GameObject> bulletPool;

    private bool shotInReload = false;
    private float shotReloadTimer = 0f;

    public void TryShoot()
    {
        if (!shotInReload)
        {
            playerAnimator.SetTrigger(Constants.SHOOT);
        }
    }

    private void Update()
    {
        if (shotInReload)
        {
            shotReloadTimer += Time.deltaTime;
            statusImage.fillAmount = shotReloadTimer / timeBetweenShots;
            if (shotReloadTimer >= timeBetweenShots)
            { 
                shotInReload = false;
                shotReloadTimer= 0;
                statusImage.fillAmount = 1;
            }
        }
    }

    public void Shoot()
    {
        if (!shotInReload)
        {
            GameObject newBullet = GetBulletFromPool();
            if (newBullet == null)
            {
                newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
                bulletPool.Add(newBullet);
            } else
            {
                newBullet.transform.position = firePoint.position;
                newBullet.SetActive(true);
            }
            
            var bulletRB = newBullet.GetComponent<Rigidbody2D>();
            var sprite = newBullet.GetComponentInChildren<SpriteRenderer>();
            if (firePoint.localPosition.x > 0)
            {
                bulletRB.velocity = new Vector2(bulletSpeed * 1, bulletRB.velocity.y);
                sprite.flipX = false;
            }
            else
            {
                
                sprite.flipX = true;
                bulletRB.velocity = new Vector2(bulletSpeed * -1, bulletRB.velocity.y);
            }
            shotInReload = true;
            movement.EndAttack();
        }
    }

    private GameObject GetBulletFromPool()
    {
        foreach(GameObject bullet in bulletPool)
        {
            if (!bullet.activeSelf)
            {
                return bullet;
            }
        }

        return null;
    }
}
