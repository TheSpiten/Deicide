using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSpawner : MonoBehaviour
{
    public GameObject Spear;
    public float spearSpeed;
    public Vector2 SpawnPos;
    private Transform spawnTransform;
    // multi is the multiplier for the distance between the spears
    public float multiX;
    public float multiY;
    public float xPos;
    public float yPos;
    
    
    
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
        spawnTransform = GameObject.Find("SpearSpawner").transform;
        for (int i = 1; i < 5; i++)
        {
            var spears = (GameObject)Instantiate(Spear, transform.position, transform.rotation);
            spears.GetComponent<Rigidbody2D>().velocity = -spears.transform.up * spearSpeed;
            SpawnPos = new Vector2((xPos + (i * multiX)), (yPos + (i * multiY)));
            spawnTransform.position = SpawnPos;
            Destroy(spears, 2.0f);
        }
        xPos = -8.13293f;
        yPos = 6.004479f;
        SpawnPos = new Vector2((xPos), (yPos));
        spawnTransform.position = SpawnPos;
    } 



}
