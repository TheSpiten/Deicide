using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossfunctions : MonoBehaviour
{
        private int counter;

        public List<Color> colors; //list of colors, can be changed in inspector

        SpriteRenderer renderReference;

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "Bullet") { //uncomment to check tag of colliding object
                if (counter < colors.Count - 1)
                {
                    counter++;
                }
                else
                {
                    counter = 0;
                }
                renderReference.color = colors[counter];
            } //uncomment to check tag of colliding object
        }

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        renderReference = GetComponent<SpriteRenderer>();
        renderReference.color = colors[counter];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
