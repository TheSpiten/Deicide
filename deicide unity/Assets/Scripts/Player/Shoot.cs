using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    int delay = 0;
    int delayDynamite = 0;
    public int dynamiteAmmo = 0;

    public GameObject Gun;
    public GameObject Bullet;
    public GameObject Dynamite;

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
    }

    void ShootBullet()
    {
        // Spawning bullet and shooting it towards the mouse
        var bullet = (GameObject)Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 20;
        Destroy(bullet, 4.0f);
        delay = 0;
        Camera.main.GetComponent<ScreenShake>().Shake(0.05f, 0.1f);
        // Plays shooting sound
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(1);
    }

    public void ShootDynamite()
    {
        // Spawning dynamite and shooting it towards the mouse
        GameObject dynamite = Instantiate(Dynamite, Gun.transform.position, Gun.transform.rotation);
        dynamite.GetComponent<Rigidbody2D>().velocity = dynamite.transform.right * 7;
        Destroy(dynamite, 4.0f);
        delayDynamite = 0;
        // Plays shooting sound
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(1);
    }
}
