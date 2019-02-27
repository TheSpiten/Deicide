using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public GameObject TestExplosionArea;
    public GameObject EnergyExplosion;
    public float delay = 2;

    //public float power = 10.0f;
    public float radius = 2f;
    //public float upforce = 1.0f;

    public bool shouldDraw = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Detonate", delay);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -180) * Time.deltaTime);
    }


    void Detonate()
    {
        // Particle effects for the explosion
        var explosion = (GameObject)Instantiate(EnergyExplosion, transform.position, transform.rotation);
        Destroy(explosion, 1.95f);
        Camera.main.GetComponent<ScreenShake>().Shake(0.2f, 0.2f);
        //Camera.current.GetComponent<ScreenShake>().Shake();

        Vector2 explosionPos = transform.position;
        // Array of colliders in the sphere
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(explosionPos, radius);
        //Instantiating a sphere to see if it spawns at the right place, works fine
        //var explArea = (GameObject)Instantiate(TestExplosionArea, explosionPos, transform.rotation);
        //Destroy(explArea, 0.5f);

        foreach (Collider2D hit in collider2Ds)
        {
            if (!hit.GetComponent<Rigidbody2D>()) { continue; }
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            // trying to do damage depending on distance
            Debug.Log(rb.gameObject.tag);
            Debug.Log(hit.bounds.SqrDistance(explosionPos));
            Debug.Log(Mathf.RoundToInt(50 * 1/(hit.bounds.SqrDistance(explosionPos) + 1)));
            if (rb.gameObject.tag == "Feather")
            {
                Destroy(rb.gameObject);
            }

            else if (rb.gameObject.tag == "EnemyHead" || rb.gameObject.tag == "EnemyLegs" || rb.gameObject.tag == "EnemyBody")
                rb.gameObject.GetComponentInParent<BossHealth>().DamageBoss(Mathf.RoundToInt(50 * 1 / (hit.bounds.SqrDistance(explosionPos) + 1)));
        }
        // Destroying the dynamite
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // TestEnemy is a script name, careful with the names if you change/reuse
            collision.gameObject.GetComponentInParent<BossHealth>().DamageBoss(10);
            Detonate();
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Detonate();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "EnemyHead" || collision.gameObject.tag == "EnemyLegs" || collision.gameObject.tag == "EnemyBody")
        {
            //collision.gameObject.GetComponent<Bossfunctions>().HitBoss(collision.gameObject.tag);
            Detonate();
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(0);
        }
    }
}

