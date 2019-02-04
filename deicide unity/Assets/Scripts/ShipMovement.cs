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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0));
        rb.AddForce(new Vector2(0, Input.GetAxisRaw("Vertical") * speed));

        if (Input.GetMouseButton(0) && delay > 20)
            Shoot();

        delay++;
    }

    void Shoot()
    {
        var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = 0;
        var aim = Quaternion.FromToRotation(Vector3.right, MousePos - transform.position);

        var bullet = (GameObject)Instantiate(Bullet, Gun.transform.position, aim);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 15;
        Destroy(bullet, 2.0f);
        delay = 0;
    }

    public void Damage()
    {
        health--;
        if (health == 0)
            Destroy(gameObject);
    }
}
