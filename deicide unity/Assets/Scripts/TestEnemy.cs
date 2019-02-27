using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed_x;
    public float speed_y;
    public float health;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Simple movement along a Vector2, change speed_y and speed_x to change direction
        //rb.velocity = new Vector2(speed_x, speed_y);

    }

    // Checking for collision with the player's ship
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Deals one damage to player's ship
            collision.gameObject.GetComponent<ShipMovement>().Damage();
            // Destroys the enemy, probably not what we want
            Die();
        }
    }

    // Deal one damage to the enemy
    public void Damage()
    {
        health--;
        if (health == 0)
            Die();
    }

    // Destroy the enemy
    void Die()
    {
        Destroy(gameObject);
    }

}
