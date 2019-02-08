using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossfunctions : MonoBehaviour
{
        private int counter;

        public List<Color> colors; //list of colors, can be changed in inspector

        public SpriteRenderer renderHead;
        public SpriteRenderer renderBody;
        public SpriteRenderer renderLegs;
    /*
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
        }*/

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        colors.Add(new Color(100f, 100f, 100f));
        renderHead.color = colors[counter];
        renderBody.color = colors[counter];
        renderLegs.color = colors[counter];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitBoss(string enemyTag)
    {
        if (counter < colors.Count - 1)
        {
            counter++;
        }
        else
        {
            counter = 0;
        }

        switch(enemyTag)
        {
            case "EnemyHead":
                renderHead.color = colors[counter];
                break;

            case "EnemyBody":
                renderBody.color = colors[counter];
                break;

            case "EnemyLegs":
                renderLegs.color = colors[counter];
                break;

        }
    }
}
