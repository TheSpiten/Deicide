using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private float spearSpeed;
    private float spearTimer;
    private float indicatorTimer;
    private GameObject privateIndicator;
    private GameObject closingIndicator;
    public GameObject IndicatorPrefab;
    public GameObject ClosingIndicatorPrefab;
    private bool hasSpawnedIndicator;
    private bool hasSpawnedClosingIndicator;
    private bool hasPlayedAudio;

    public void Start()
    {
        privateIndicator = null;
        hasSpawnedIndicator = false;
        hasPlayedAudio = false;
        indicatorTimer = 1.8f;
        hasSpawnedClosingIndicator = false;
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

            if (hasPlayedAudio == false)
            {
                hasPlayedAudio = true;

                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(17);
            }
        }

        if (spearTimer <= 0.5f)
        {
            if (indicatorTimer > 0)
            {
                indicatorTimer -= Time.deltaTime;
            }
            else if (hasSpawnedClosingIndicator == false)
            {
                closingIndicator = GameObject.Instantiate(ClosingIndicatorPrefab);
                closingIndicator.transform.position = new Vector2(transform.position.x, 4.94f);
                closingIndicator.transform.parent = null;
                Destroy(closingIndicator, 0.2f);
                hasSpawnedClosingIndicator = true;
            }

            if (privateIndicator == null && hasSpawnedIndicator == false)
            {
                privateIndicator = (GameObject) Instantiate(IndicatorPrefab);
                privateIndicator.transform.position = new Vector2(transform.position.x, 4.94f);
                privateIndicator.transform.parent = null;
                Destroy(privateIndicator, 1.8f);
                hasSpawnedIndicator = true;
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<ShipMovement>().isShielded == false)
        {
            // ShipMovement is a script name, careful with the names if you change/reuse
            collision.gameObject.GetComponent<ShipMovement>().Damage(20);
            Destroy(gameObject);
        }
    }
}
