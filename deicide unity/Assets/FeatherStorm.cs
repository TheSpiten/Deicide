using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherStorm : MonoBehaviour
{
    public GameObject Feather;
    public GameObject FeatherSpawn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            FeatherAttack();
        }


    }

    void FeatherAttack()
    {
        var feather = (GameObject)Instantiate(Feather, FeatherSpawn.transform.position, FeatherSpawn.transform.rotation);
        feather.GetComponent<Rigidbody2D>().velocity = -feather.transform.right * 7;
        Destroy(feather, 4.0f);
    }
}
