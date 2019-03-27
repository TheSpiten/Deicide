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
    public int numberOfSpears;
    public int numberOfRandomSpears;
    
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
            SpearInstantiate("Left");
        }

        
    }

    public void SpearInstantiate(string rainType)
    {
        switch (rainType)
        {
            case "Left":
                spawnTransform = GameObject.Find("SpearSpawner").transform;
                for (int i = 1; i <= numberOfSpears; i++)
                {
                    var spears = (GameObject)Instantiate(Spear);
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
                break;

            case "Right":
                spawnTransform = GameObject.Find("SpearSpawner").transform;
                for (int i = numberOfSpears; i >= 1; i--)
                {
                    SpawnPos = new Vector2((xPos + (i * multiX)), yPos);
                    spawnTransform.position = SpawnPos;
                    var spears = (GameObject)Instantiate(Spear);
                    spears.transform.position = new Vector2(transform.position.x, transform.position.y + 3.65f);
                    spears.GetComponent<Spear>().SetSpear(numberOfSpears - i, spearSpeed, 0.2f);
                    Destroy(spears, 10.0f);
                    //spears.GetComponent<Rigidbody2D>().velocity = -spears.transform.up * spearSpeed;
                }
                xPos = -8.13293f;
                yPos = 6.004479f;
                SpawnPos = new Vector2((xPos), (yPos));
                spawnTransform.position = SpawnPos;
                break;

            case "Random":
                int xOffset = Mathf.FloorToInt((numberOfSpears - numberOfRandomSpears) / 2);
                spawnTransform = GameObject.Find("SpearSpawner").transform;
                List<int> spearSpawnList = new List<int>();
                for (int p = 1; p <= numberOfSpears; p++)
                {
                    spearSpawnList.Add(p);
                }
                for (int i = 1; i <= numberOfRandomSpears; i++)
                {
                    Debug.Log(spearSpawnList.Count);
                    int index = 0;
                    int spawn = 0;
                    var spears = (GameObject)Instantiate(Spear);
                    spears.transform.position = new Vector2(transform.position.x, transform.position.y + 3.65f);
                    // Set the 0 to 1 to remove the gap in the spears
                    index = Mathf.FloorToInt(Random.Range(0, spearSpawnList.Count - 0.0001f));
                    spawn = spearSpawnList[index];
                    spearSpawnList.RemoveAt(index);
                    //spears.GetComponent<Rigidbody2D>().velocity = -spears.transform.up * spearSpeed;
                    SpawnPos = new Vector2((xPos + (spawn * multiX)), yPos);
                    spawnTransform.position = SpawnPos;
                    spears.GetComponent<Spear>().SetSpear(spawn, spearSpeed, 0.2f);
                    Destroy(spears, 10.0f);
                }
                xPos = -8.13293f;
                yPos = 6.004479f;
                SpawnPos = new Vector2((xPos), (yPos));
                spawnTransform.position = SpawnPos;
                break;
        }
    } 



}
