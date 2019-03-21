using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSpawner : MonoBehaviour
{
    public GameObject Spear;
    public float spearSpeed;
    private Vector2 SpawnPos;
    private Transform spawnTransform;
    // multi is the multiplier for the distance between the spears
    public float multiX;
    public float multiY;
    private float xPos;
    private float yPos;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
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
        for (int i = 1; i <= 8; i++)
        {
            var spears = (GameObject) Instantiate(Spear);
            spears.transform.position = new Vector2(transform.position.x, transform.position.y + 3.65f);
            //spears.GetComponent<Rigidbody2D>().velocity = -spears.transform.up * spearSpeed;
            SpawnPos = new Vector2((xPos + (i * multiX)), yPos);
            spawnTransform.position = SpawnPos;
            spears.GetComponent<Spear>().SetSpear(i, spearSpeed, 0.2f);
            Destroy(spears, 10.0f);
        }
        xPos = -8.13293f;
        yPos = 6.004479f;
        SpawnPos = new Vector2((xPos), (yPos));
        spawnTransform.position = SpawnPos;
    } 



}
