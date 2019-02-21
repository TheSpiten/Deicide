using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherStorm : MonoBehaviour
{
    public GameObject Feather;
    public GameObject FeatherSpawn;
    public float ConeSize = 3;
    public float FeatherAmount = 5;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            FeatherAttack();
        }


    }

    void FeatherAttack()
    {
        float xSpread = Random.Range(-3, 3);
        float ySpread = Random.Range(-3, 3);
        //normalize the spread vector to keep it conical
        Vector3 spread = new Vector3(xSpread, ySpread, 0.0f).normalized * ConeSize;
        Quaternion rotation = Quaternion.Euler(spread) * FeatherSpawn.transform.rotation;

        for (int i = 0; i < FeatherAmount; i++) 
        {
            var feather = (GameObject)Instantiate(Feather, FeatherSpawn.transform.position, rotation);
            feather.GetComponent<Rigidbody2D>().velocity = -feather.transform.right * 7;
            Destroy(feather, 3.0f);
        }
    }
}
