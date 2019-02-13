using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<Bossfunctions>().HitBoss(collision.gameObject.tag);
            //Destroy(collision);
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(0);
        }







        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
