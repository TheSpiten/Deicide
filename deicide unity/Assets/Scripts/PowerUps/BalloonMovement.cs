using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 move;
    float angle = 0.0f;
    float rotation = 10.0f;
    float rotationSpeed = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        move = new Vector3(0.0f, 1.5f, 0.0f);
    }

    void FixedUpdate()
    {
        angle = Mathf.Sin(Time.time * rotationSpeed) * rotation;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
        transform.position += move * Time.deltaTime;
    }
}
