using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    int delay = 0;
    int delayDynamite = 0;
    int flareDelay = 0;
    public int dynamiteAmmo = 0;

    public GameObject Gun;
    public GameObject Bullet;
    public GameObject Dynamite;
    //public GameObject MuzzleFlare;

    private void Awake()
    {
        Gun = transform.Find("GunRotator").Find("Gun").Find("BulletSpawn").gameObject;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (gameObject.GetComponent<ShipMovement>().GetPlayerAlive() == true)
        {
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Comma))
            {
                if (delay > 10)
                {
                    ShootBullet();
                }
            }

            if (flareDelay >= 15)
            //if (Input.GetKey(KeyCode.Comma) == false)
            {
                transform.Find("MuzzleFlash01").gameObject.SetActive(false);
                flareDelay = 0;
            }

            if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Period))
            {
                if (delayDynamite > 40 && dynamiteAmmo > 0)
                {
                    ShootDynamite();
                    dynamiteAmmo--;
                }
            }
        }

        delay++;
        delayDynamite++;
        flareDelay++;
    }

    void ShootBullet()
    {
        transform.Find("MuzzleFlash01").gameObject.SetActive(true);
        // Spawning bullet and shooting it towards the mouse
        //var flare = (GameObject)Instantiate(MuzzleFlare, Gun.transform.position, Gun.transform.rotation);
        //flare.transform.position = Gun.transform.position;
        var bullet = (GameObject)Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 20;
        Destroy(bullet, 4.0f);
        delay = 0;
        //Camera.main.GetComponent<ScreenShake>().Shake(0.025f, 0.1f);
        // Plays shooting sound
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(1);
    }

    public void ShootDynamite()
    {
        transform.Find("MuzzleFlash01").gameObject.SetActive(true);
        // Spawning dynamite and shooting it towards the mouse
        //var flare = (GameObject)Instantiate(MuzzleFlare, Gun.transform.position, Gun.transform.rotation);
        GameObject dynamite = Instantiate(Dynamite, Gun.transform.position, Quaternion.AngleAxis(45.0f, Vector3.forward).normalized * Gun.transform.rotation);
        dynamite.GetComponent<Rigidbody2D>().velocity = dynamite.transform.right * 7;
        Destroy(dynamite, 4.0f);
        delayDynamite = 0;
        // Plays shooting sound
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(1);
    }
}
