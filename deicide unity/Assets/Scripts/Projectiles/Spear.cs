using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private float spearSpeed;
    private float spearTimer;
    private GameObject privateIndicator;
    public GameObject IndicatorPrefab;
    private bool hasSpawnedIndicator;

    public void Start()
    {
        privateIndicator = null;
        hasSpawnedIndicator = false;
    }

    public void SetSpear(float timer, float speed, float delay){
        spearSpeed = speed;
        spearTimer = timer * delay + 1;
    }

    public void Update()
    {
        if (spearTimer > 0)
        {
            spearTimer -= Time.deltaTime;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, Mathf.Abs(spearSpeed) * -1);
        }

        if (spearTimer <= 0.5f)
        {
            if (privateIndicator == null && hasSpawnedIndicator == false)
            {
                privateIndicator = (GameObject) Instantiate(IndicatorPrefab, transform);
                privateIndicator.transform.position = new Vector2(transform.position.x, 4.9f);
                privateIndicator.transform.parent = null;
                Destroy(privateIndicator, 1f);
                hasSpawnedIndicator = true;
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // ShipMovement is a script name, careful with the names if you change/reuse
            collision.gameObject.GetComponent<ShipMovement>().Damage(20);
            Destroy(gameObject);
        }
    }
}
