﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject shieldPrefab;
    bool hasShield = false;
    bool hasHPack = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ShieldPowerUp"))
        {
            other.gameObject.SetActive(false);
            hasShield = true;
            SetOthersToFalse("shield");
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(4);
            //Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("HealthPowerUp"))
        {
            other.gameObject.SetActive(false);
            hasHPack = true;
            SetOthersToFalse("health");
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(4);
            //Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("DynamitePowerUp"))
        {
            other.gameObject.SetActive(false);
            gameObject.GetComponent<Shoot>().dynamiteAmmo = 5;
            SetOthersToFalse("dynamite");
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(4);
            //Destroy(other.gameObject);
        }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("ShieldPowerUp"))
    //    {
    //        //other.gameObject.SetActive(false);
    //        hasShield = true;
    //        SetOthersToFalse("shield");
    //        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(4);
    //        Destroy(collision.gameObject);
    //    }
    //    else if (collision.gameObject.CompareTag("HealthPowerUp"))
    //    {
    //        //other.gameObject.SetActive(false);
    //        hasHPack = true;
    //        SetOthersToFalse("health");
    //        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(4);
    //        Destroy(collision.gameObject);
    //    }
    //    else if (collision.gameObject.CompareTag("DynamitePowerUp"))
    //    {
    //        //other.gameObject.SetActive(false);
    //        gameObject.GetComponent<Shoot>().dynamiteAmmo = 5;
    //        SetOthersToFalse("dynamite");
    //        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(4);
    //        Destroy(collision.gameObject);
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            if (hasShield == true)
            {
                Shield();
                hasShield = false;
            }
            else if (hasHPack == true)
            {
                Heal();
                hasHPack = false;
            }
            else if (GetComponent<Shoot>().dynamiteAmmo > 0)
                GetComponent<Shoot>().ShootDynamite();
        }
    }



    void Heal()
    {
        if (gameObject.GetComponent<ShipMovement>().health < 100)
        {
            gameObject.GetComponent<ShipMovement>().health += 25;
        }
    }

    void Shield()
    {
        // I'm guessing this won't follow the ship and will have to be changed, but not sure so maybe ?
        var shield = (GameObject)Instantiate(shieldPrefab, transform.position, transform.rotation);
        shield.transform.up = transform.up;
        shield.transform.right = transform.right;
    }

    void SetOthersToFalse(string powerup)
    {
        if (powerup == "dynamite")
        {
            hasHPack = false;
            hasShield = false;
        }
        else if (powerup == "shield")
        {
            hasHPack = false;
            GetComponent<Shoot>().dynamiteAmmo = 0;
        }
        else if (powerup == "health")
        {
            hasShield = false;
            GetComponent<Shoot>().dynamiteAmmo = 0;
        }
    }
}
