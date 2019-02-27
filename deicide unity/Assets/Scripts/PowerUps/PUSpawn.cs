using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUSpawn : MonoBehaviour
{
    public GameObject ShieldBalloon;
    public GameObject HealthBalloon;
    public GameObject DynamiteBalloon;
    int PUdelay = 0;

    private void FixedUpdate()
    {
        PUdelay++;
        if (PUdelay > 200)
        {
            SpawnPU();
            PUdelay = 0;
        }
    }

    void SpawnPU()
    {
        int index = Random.Range(1, 4);
        Vector2 spawnPos = new Vector2(Random.Range(-5.0f, 0.0f), -6.0f);
        if (index == 1)
        {
            var shieldballoon = (GameObject)Instantiate(ShieldBalloon, spawnPos, Quaternion.Euler(0, 0, 0));
            //shieldballoon.GetComponent<Rigidbody2D>().velocity = shieldballoon.transform.up * 1.5f;
            //shieldballoon.GetComponent<Rigidbody2D>().velocity = shieldballoon.transform.right * Random.Range(-1.0f, 1.0f);
            //shieldballoon.GetComponent<Rigidbody2D>().velocity.Set(Random.Range(-1.0f, 1.0f), 7.0f);
            Destroy(shieldballoon, 8.0f);
        }
        else if (index == 2)
        {
            var healthballoon = (GameObject)Instantiate(HealthBalloon, spawnPos, Quaternion.Euler(0, 0, 0));
            //healthballoon.GetComponent<Rigidbody2D>().velocity = healthballoon.transform.up * 1.5f;
            //healthballoon.GetComponent<Rigidbody2D>().velocity = healthballoon.transform.right * Random.Range(-1.0f, 1.0f);
            Destroy(healthballoon, 8.0f);
        }
        else if (index == 3)
        {
            var dynamiteballoon = (GameObject)Instantiate(DynamiteBalloon, spawnPos, Quaternion.Euler(0, 0, 0));
            //dynamiteballoon.GetComponent<Rigidbody2D>().velocity = dynamiteballoon.transform.up * 1.5f;
            //dynamiteballoon.GetComponent<Rigidbody2D>().velocity = dynamiteballoon.transform.right * Random.Range(-1.0f, 1.0f);
            Destroy(dynamiteballoon, 8.0f);
        }
    }
}
