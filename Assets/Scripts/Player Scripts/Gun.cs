using UnityEngine;

public class Gun : MonoBehaviour
{
    public int upgradeLevelRequirement = 0;

    public GunScript bullet;
    Vector2 direction;

    public bool autoShoot = false;
    public float shootInterval = 0.5f;
    public float shootDelay = 0.0f;
    public float shootTimer = 0f;
    public float delayTimer = 0f;

    public bool isActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        direction = (transform.localRotation * Vector2.right).normalized;

        if (autoShoot)
        {
            if (delayTimer >= shootDelay)
            {
                if (shootTimer >= shootInterval)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }

        }
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        GunScript goGunScript = go.GetComponent<GunScript>(); 
        goGunScript.direction = direction; 
    }
}
