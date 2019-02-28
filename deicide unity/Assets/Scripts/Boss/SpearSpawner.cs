using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSpawner : MonoBehaviour
{
    public GameObject Spear;
    public float spearSpeed;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpearInstantiate();
        }

        
    }

    public void SpearInstantiate()
    {
        for (int i = 0; i < 4; i++)
        {
            var spears = (GameObject)Instantiate(Spear, transform.position, transform.rotation);
            spears.GetComponent<Rigidbody2D>().velocity = -spears.transform.up * spearSpeed;
            Destroy(spears, 2.0f);
        }
    } 



}
