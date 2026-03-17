using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyDeath : MonoBehaviour
{

   public GameObject explosion;
    bool destroyable = false;
    public int scoreValue = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Level.instance.AddEnemy();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position .x < -11)
        {
            DestroyDestroyable();
        }

        if (transform.position.x < 8.5f && !destroyable)
        {
            destroyable = true;
            Gun[] guns = transform.GetComponentsInChildren<Gun>();
            foreach (Gun gun in guns)
            {
                gun.isActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!destroyable)
        {
            return;
        }

       GunScript projectile = collision.GetComponent<GunScript>();
        if (projectile != null)
        {
            if (!projectile.isEnemy)
            {
                Level.instance.AddScore(scoreValue);
                DestroyDestroyable();
                Destroy(projectile.gameObject);
            }
        }
    }

    void DestroyDestroyable()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Level.instance.RemoveEnemy();
        Destroy(gameObject);
    }
}
