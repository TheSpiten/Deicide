using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    int delay = 0;
    int delayDynamite = 0;
    public GameObject Gun;
    public GameObject Bullet;
    public GameObject Dynamite;
    Rigidbody2D rb;
    public float speed;
    public float dashDelay;
    public float dashMultiplier;
    public float dragMultiplier;
    public float dashSpeedIncrease;
    public float dashSpeedDuration;
    int health = 3;
    private float dashTimer;
    private float dashSpeedTimer;
    private float dashCurrentIncreasedSpeed;

    public bool shootAim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        Gun = transform.Find("GunRotator").Find("Gun").Find("BulletSpawn").gameObject;
=======
        if (shootAim == true)
        {
            Gun = transform.Find("Sphere").Find("Gun").gameObject;
        }
        else
        {
            Gun = transform.Find("Gun").gameObject;
        }
>>>>>>> master
        dashCurrentIncreasedSpeed = 1;
    }

    // Should maybe be FixedUpdate()?
    void FixedUpdate()
    {
        // Updates dash speed
        float newVelocityX = rb.velocity.x;
        float newVelocityY = rb.velocity.y;
        int signVelocityX = Mathf.FloorToInt(Mathf.Sign(rb.velocity.x));
        int signVelocityY = Mathf.FloorToInt(Mathf.Sign(rb.velocity.y));
        if (Mathf.Abs(rb.velocity.x) > speed)
        {
            newVelocityX = rb.velocity.x - (rb.drag * dragMultiplier * Mathf.Sign(rb.velocity.x));
            if (Mathf.Sign(newVelocityX) != signVelocityX)
            {
                newVelocityX = 0;
            }
        }
        if (Mathf.Abs(rb.velocity.y) > speed)
        {
            newVelocityY = rb.velocity.y - (rb.drag * dragMultiplier * Mathf.Sign(rb.velocity.y));
            if (Mathf.Sign(newVelocityY) != signVelocityY)
            {
                newVelocityY = 0;
            }
        }
        rb.velocity = new Vector2(newVelocityX, newVelocityY);

        // Getting input for horizontal movement
        float move_h = Input.GetAxisRaw("Horizontal");
        // Getting input for vertical movement
        float move_v = Input.GetAxisRaw("Vertical");

        // Normalizing the vector so diagonal movement isn't faster
        Vector2 movement = new Vector2(move_h, move_v).normalized;

        // Making the object move by adding a force to it
        rb.AddForce(movement * speed * dashCurrentIncreasedSpeed);
    }

    private void Update()
    {
        // Updates dashTimer
        dashTimer = TimerTick(dashTimer);
        // Updates dashSpeedTimer
        dashSpeedTimer = TimerTick(dashSpeedTimer);

        // Declares input keys
        var shootKey = KeyCode.Z;
        var dashKey = KeyCode.Space;
        var powerupKey = KeyCode.C;

        // Shooting on left click. Modify number after "delay >" to change the time between shots
        // Also shoots if shootKey is pressed
        if (Input.GetMouseButton(0) || Input.GetKey(shootKey))
        {
            if (delay > 20)
            {
                if (shootAim == true)
                {
                    Shoot();
                }
                else
                {
                    ShootNoAim();
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (delayDynamite > 40)
            {
                ShootDynamite();
            }
        }

        // Dashes if dashKey(or right click ?) is pressed
        if (Input.GetMouseButton(2) || Input.GetKeyDown(dashKey))
            {
                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    if (dashTimer == 0)
                    {
                        Dash();
                        dashTimer = dashDelay;
                    }
                }
            }

        // Resets speed increase caused by dashing
        if (dashSpeedTimer == 0)
        {
            dashCurrentIncreasedSpeed = 1;
        }

        delay++;
        delayDynamite++;
    }

    void Shoot()
    {
        // Checking for mouse position and making a Quaternion of it
        //var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //MousePos.z = 0;
        //var aim = Quaternion.FromToRotation(Vector3.right, MousePos - transform.position);

        // Spawning bullet and shooting it towards the mouse
        var bullet = (GameObject)Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 15;
        Destroy(bullet, 2.0f);
        delay = 0;
        // Plays shooting sound
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(1);
    }

    private void ShootNoAim()
    {
        var bullet = (GameObject)Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 15;
        Destroy(bullet, 2.0f);
        delay = 0;
        // Plays shooting sound
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(1);
    }

    void ShootDynamite()
    {
        // Checking for mouse position and making a Quaternion of it
        //var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //MousePos.z = 0;
        //var aim = Quaternion.FromToRotation(Vector3.right, MousePos - transform.position);

        // Spawning dynamite and shooting it towards the mouse
        var dynamite = (GameObject)Instantiate(Dynamite, Gun.transform.position, Gun.transform.rotation);
        dynamite.GetComponent<Rigidbody2D>().velocity = dynamite.transform.right * 7;
        Destroy(dynamite, 4.0f);
        delayDynamite = 0;
        // Plays shooting sound
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(1);
    }

    private void Dash()
    {
        rb.velocity += new Vector2(Input.GetAxisRaw("Horizontal") * speed * dashMultiplier, Input.GetAxisRaw("Vertical") * speed * dashMultiplier);

        dashCurrentIncreasedSpeed = dashSpeedIncrease;
        dashSpeedTimer = dashSpeedDuration;
    }

    // Ship takes 1 damage
    public void Damage()
    {
        health--;
        if (health == 0)
            Destroy(gameObject);
    }

    private float TimerTick(float currentTime)
    {
        float returnTime = currentTime;
        // Updates timer
        if (currentTime > 0)
        {
            returnTime -= Time.deltaTime;
        }
        if (currentTime < 0)
        {
            returnTime = 0;
        }
        return returnTime;
    }
}
