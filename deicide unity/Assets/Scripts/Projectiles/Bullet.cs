using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Only checking for collision with object tagged "Enemy"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Enemy")
        //{
        //    // TestEnemy is a script name, careful with the names if you change/reuse
        //    collision.gameObject.GetComponent<BossHealth>().DamageBoss(200);
        //    Destroy(gameObject);
        //}
        if (collision.gameObject.tag == "EnemyHead" || collision.gameObject.tag == "EnemyLegs" || collision.gameObject.tag == "EnemyBody")
        {
            //collision.gameObject.GetComponent<Bossfunctions>().HitBoss(collision.gameObject.tag);
            collision.gameObject.GetComponentInParent<BossHealth>().DamageBoss(10);
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(0);
        }

        if (collision.gameObject.tag == "BossShield")
        {
            Transform bouncePoint = collision.gameObject.transform.Find("ShieldBouncePoint");
            Vector2 bounceDirection = new Vector2(transform.position.x - bouncePoint.position.x, transform.position.y - bouncePoint.position.y);
            bounceDirection.Normalize();
            transform.rotation = Quaternion.Euler(bounceDirection);
            gameObject.GetComponent<Rigidbody2D>().velocity = bounceDirection * 15;
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(7);
        }
    }
}