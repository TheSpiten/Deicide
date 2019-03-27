using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipMovement : MonoBehaviour
{
    public GameObject FireExplosion;

    Rigidbody2D rb;
    public float speed;
    public float dashDelay;
    public float dashMultiplier;
    public float dragMultiplier;
    public float dashSpeedIncrease;
    public float dashSpeedDuration;
    public int health = 100;
    private float iTimer;
    public float iFrames;
    private float dashTimer;
    private float dashSpeedTimer;
    private float dashCurrentIncreasedSpeed;

    private bool alive;

    public bool isShielded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        dashCurrentIncreasedSpeed = 1;

        alive = true;

        iTimer = 0;
    }

    // Should maybe be FixedUpdate()?
    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().GameStarting() == false)
        {
            if (alive == true)
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
        }
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().GameStarting() == false)
        {
            if (alive == true)
            {
                if (transform.Find("shieldPrefab").gameObject.activeSelf == true && transform.Find("shieldBack").gameObject.activeSelf == true)
                    isShielded = true;
                else if ((transform.Find("shieldPrefab").gameObject.activeSelf == true && transform.Find("shieldBack").gameObject.activeSelf == true) == false)
                    isShielded = false;

                // Updates dashTimer
                dashTimer = TimerTick(dashTimer);
                // Updates dashSpeedTimer
                dashSpeedTimer = TimerTick(dashSpeedTimer);

                // Declares input keys
                var shootKey = KeyCode.Z;
                var dashKey = KeyCode.Space;
                var powerupKey = KeyCode.C;


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

                // Counts iFrames
                if (iTimer > 0)
                {
                    iTimer -= Time.deltaTime;
                }
            }
        }
    }

    private void Dash()
    {
        rb.velocity += new Vector2(Input.GetAxisRaw("Horizontal") * speed * dashMultiplier, Input.GetAxisRaw("Vertical") * speed * dashMultiplier);

        dashCurrentIncreasedSpeed = dashSpeedIncrease;
        dashSpeedTimer = dashSpeedDuration;
    }

    // Ship takes 1 damage
    public void Damage(int damage)
    {
        if (iTimer <= 0)
        {
            health -= damage;
            iTimer = iFrames;
            if (health <= 0)
            {
                if (alive == true)
                {
                    alive = false;
                    GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().TurnOffUI();
                    //transform.Find("ShipSprite").gameObject.SetActive(false);
                    //transform.Find("GunRotator").transform.Find("Gun").gameObject.SetActive(false);
                    Time.timeScale = 0.25f;
                    Destroy(gameObject, 0.1f);
                    //SceneManager.LoadScene("MenuScene");
                    var explosion = (GameObject)Instantiate(FireExplosion, transform.position, transform.rotation);
                }
            }
        }
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

    public float GetPlayerHealth()
    {
        return health;
    }

    public float GetPlayerDashTimer()
    {
        return dashTimer;
    }

    public bool GetPlayerAlive()
    {
        return alive;
    }
}
