using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Only checking for collision with object tagged "Enemy"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // TestEnemy is a script name, careful with the names if you change/reuse
            collision.gameObject.GetComponent<TestEnemy>().Damage();
            Destroy(gameObject);
        }
    }
}