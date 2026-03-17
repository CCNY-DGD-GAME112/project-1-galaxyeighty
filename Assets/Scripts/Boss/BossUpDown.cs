using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BossUpDown : MonoBehaviour
{
    public float moveSpeed = 5;
    private Rigidbody2D rb;
    private Vector2 initialPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        initialPosition = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
            float newY = Mathf.Sin(Time.time * moveSpeed);
            Vector2 position = new Vector2(0, newY) + initialPosition;
            rb.MovePosition(position); 
    }
}
