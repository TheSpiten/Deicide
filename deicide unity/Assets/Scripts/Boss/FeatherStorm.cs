using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherStorm : MonoBehaviour
{
    public GameObject Feather;
    public GameObject FeatherSpawn;
    public float ConeSize = 4;
    public float FeatherAmount = 5;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            FeatherAttack();
        }


    }

    public void FeatherAttack()
    {

        var feather = (GameObject)Instantiate(Feather, FeatherSpawn.transform.position, FeatherSpawn.transform.rotation);
        feather.GetComponent<Rigidbody2D>().velocity = -feather.transform.right * 8;


        for (int i = 1; i < FeatherAmount; i++) 
        {
            //float xSpread = Random.Range(-3, 3);
            //float ySpread = Random.Range(-3, 3);
            //Vector3 spread = new Vector3(xSpread, ySpread, 0.0f).normalized * ConeSize;
            Quaternion rotation = Quaternion.AngleAxis(Random.Range(-30,30), Vector3.forward).normalized * FeatherSpawn.transform.rotation;

            var feathers = (GameObject)Instantiate(Feather, FeatherSpawn.transform.position, rotation);
            feathers.GetComponent<Rigidbody2D>().velocity = -feathers.transform.right * Random.Range(6,9);
            Destroy(feathers, 4.0f);
        }
        Destroy(feather, 4.0f);
    }
}
