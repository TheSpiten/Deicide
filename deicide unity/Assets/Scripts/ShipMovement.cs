using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    int delay = 0;
    public GameObject Gun;
    public GameObject Bullet;
    Rigidbody2D rb;
    public float speed;
    int health = 3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Gun = transform.Find("Gun").gameObject;
    }

    void Update()
    {
        // Getting input for movement
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0));
        rb.AddForce(new Vector2(0, Input.GetAxisRaw("Vertical") * speed));

        // Shooting on left click. Modify number after "delay >" to change the time between shots
        if (Input.GetMouseButton(0) && delay > 20)
            Shoot();

        delay++;
    }

    void Shoot()
    {
        // Checking for mouse position and making a Quaternion of it
        var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = 0;
        var aim = Quaternion.FromToRotation(Vector3.right, MousePos - transform.position);

        // Spawning bullet and shooting it towards the mouse
        var bullet = (GameObject)Instantiate(Bullet, Gun.transform.position, aim);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 15;
        Destroy(bullet, 2.0f);
        delay = 0;
    }

    // Ship takes 1 damage
    public void Damage()
    {
        health--;
        if (health == 0)
            Destroy(gameObject);
    }
}
