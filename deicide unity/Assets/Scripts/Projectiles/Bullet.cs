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
            collision.gameObject.GetComponentInParent<BossHealth>().DamageBoss(50);
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(0);
        }
    }
}