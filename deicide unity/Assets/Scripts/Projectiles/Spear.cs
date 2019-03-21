using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private float spearSpeed;
    private float spearTimer;

    public void SetSpear(float timer, float speed, float delay){
        spearSpeed = speed;
        spearTimer = timer * delay;
    }

    public void Update()
    {
        if (spearTimer > 0)
        {
            spearTimer -= Time.deltaTime;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, Mathf.Abs(spearSpeed) * -1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // ShipMovement is a script name, careful with the names if you change/reuse
            collision.gameObject.GetComponent<ShipMovement>().Damage(20);
            Destroy(gameObject);
        }
    }
}
