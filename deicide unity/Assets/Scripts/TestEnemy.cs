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
        rb.velocity = new Vector2(speed_x, speed_y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<ShipMovement>().Damage();
            Die();
        }
    }

    void Damage()
    {
        health--;
        if (health == 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
