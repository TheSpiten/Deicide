using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject shieldPrefab;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShieldPowerUp"))
        {
            other.gameObject.SetActive(false);
            Shield();
        }
        else if (other.gameObject.CompareTag("HealthPowerUp"))
        {
            other.gameObject.SetActive(false);
            Heal();
        }
        else if (other.gameObject.CompareTag("DynamitePowerUp"))
        {
            other.gameObject.SetActive(false);
            gameObject.GetComponent<ShipMovement>().dynamiteAmmo += 5;
        }

    }

    void Heal()
    {
        if (gameObject.GetComponent<ShipMovement>().health < 3)
        {
        gameObject.GetComponent<ShipMovement>().health += 1;
        }
    }

    void Shield()
    {
        // I'm guessing this won't follow the ship and will have to be changed, but not sure so maybe ?
        var shield = (GameObject)Instantiate(shieldPrefab, transform.position, transform.rotation);
    }
}
