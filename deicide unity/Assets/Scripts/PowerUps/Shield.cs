using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    float timer = 0.0f;
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > 2.0f)
        {
            gameObject.SetActive(false);
            timer = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Feather") == true || collision.CompareTag("Javelin") == true)
        {
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
