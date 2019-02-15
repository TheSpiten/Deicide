using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossfunctions : MonoBehaviour
{
    public GameObject javelin;
    public GameObject javelinSpawnPoint;
    public float jabInterval;
    private float jabCountdown;

        private int counter;

        public List<Color> colors; //list of colors, can be changed in inspector

        public SpriteRenderer renderHead;
        public SpriteRenderer renderBody;
        public SpriteRenderer renderLegs;

    private SpriteRenderer spriteRenderer;
    public Sprite empty;
    public Sprite standardSprite;
    public Animator jabAnimator;
    private float jabMax = 1;
    private float jabTimer = 0;

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

    private void Awake()
    {
        jabCountdown = jabInterval;

        spriteRenderer = GetComponent<SpriteRenderer>();
        jabAnimator.gameObject.SetActive(false);
    }

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
        if (jabTimer > 0)
        {
            jabTimer -= Time.deltaTime;
        }
        else
        {
            spriteRenderer.enabled = true;
            jabAnimator.gameObject.SetActive(false);

            if (jabCountdown <= 0)
            {
                Instantiate(javelin, javelinSpawnPoint.transform);
                jabCountdown = jabInterval;
                spriteRenderer.enabled = false;
                jabAnimator.gameObject.SetActive(true);
                jabTimer = jabMax;
            }

            jabCountdown -= Time.deltaTime;

            spriteRenderer.sprite = standardSprite;
        }
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
