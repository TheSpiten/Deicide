using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherDamage : MonoBehaviour
{
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
