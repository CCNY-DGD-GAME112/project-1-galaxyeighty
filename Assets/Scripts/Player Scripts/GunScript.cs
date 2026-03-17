using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Vector2 direction = new Vector2(1, 0);
    public float speed = 6;

    public Vector2 velocity;

    public bool isEnemy = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 3);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }
}
