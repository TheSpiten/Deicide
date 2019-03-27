using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinBehavior : MonoBehaviour
{
    public float javelinDuration;
    private float javelinTimer;
    private bool hit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hit == false)
        {
            if (collision.GetComponent<ShipMovement>().isShielded == false)
            {
                collision.gameObject.GetComponent<ShipMovement>().Damage(40);
                //collision.gameObject.GetComponent<Bossfunctions>().HitBoss(collision.gameObject.tag);
                //Destroy(collision);
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(3);
                hit = true;
            }
        }
    }

    void Awake()
    {
        javelinTimer = javelinDuration;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (javelinTimer <= 0)
        {
            Destroy(gameObject);
        }
        javelinTimer -= Time.deltaTime;
    }
}
