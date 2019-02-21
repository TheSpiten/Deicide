using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossfunctions : MonoBehaviour
{
    // Boss state and general attack variables
    enum AttackType { Jab, Storm, Spears }
    enum JabState { Track, Target, Strike }
    enum BossPhase { Normal0, Enraged0 }
    enum Action { None, JabTrack, JabTarget, JabStrike, StormWait, StormFeathers }

    // Declares base attack class
    private class Attack
    {
        protected AttackType attackType;
        protected Action action;
        protected bool end;

        public Attack(AttackType type)
        {
            attackType = type;
            action = Action.None;
            end = false;
        }

        public AttackType GetAttackType()
        {
            return attackType;
        }

        public virtual void Update()
        {

        }

        public virtual Action GetAction()
        {
            return action;
        }

        public bool End()
        {
            return end;
        }
    }

    // Declares list of attacks
    private List<Attack> attackStack;

    // Declares derived JabClass
    private class JabClass : Attack
    {
        private float trackTimer;
        private float jabTimer;
        private float endTimer;
        private JabState state;

        public JabClass(float timer) : base(AttackType.Jab)
        {
            trackTimer = timer;
            jabTimer = trackTimer / 4;
            endTimer = jabTimer;
            state = JabState.Track;
            action = Action.JabTrack;
        }

        public override void Update()
        {
            switch (state)
            {
                case JabState.Track:
                    if (trackTimer > 0)
                    {
                        action = Action.JabTrack;
                        trackTimer -= Time.deltaTime;
                    }
                    else
                    {
                        state = JabState.Target;
                    }
                    break;

                case JabState.Target:
                    if (jabTimer > 0)
                    {
                        action = Action.JabTarget;
                        jabTimer -= Time.deltaTime;
                    }
                    else
                    {
                        state = JabState.Strike;
                    }
                    break;

                case JabState.Strike:
                    if (endTimer > 0)
                    {
                        if (action != Action.None)
                        {
                            action = Action.JabStrike;
                        }
                        endTimer -= Time.deltaTime;
                    }
                    else
                    {
                        action = Action.None;
                        end = true;
                    }
                    break;
            }
            
        }

        public override Action GetAction()
        {
            Action returnAction = action;

            if (action == Action.JabStrike)
            {
                action = Action.None;
            }

            return returnAction;
        }
    }

    private class StormClass : Attack
    {
        private float stormDuration;
        private float stormSpeed;
        private float featherTimer;

        public StormClass(float duration, float speed) : base(AttackType.Storm)
        {
            stormDuration = duration;
            stormSpeed = speed;
            featherTimer = stormSpeed;
            action = Action.StormWait;
        }

        public override void Update()
        {
            if (stormDuration > 0)
            {
                action = Action.StormWait;
                stormDuration -= Time.deltaTime;
                if (featherTimer > 0)
                {
                    featherTimer -= Time.deltaTime;
                }
                else
                {
                    action = Action.StormFeathers;
                }
            }
            else
            {
                end = true;
            }
        }

        public override Action GetAction()
        {
            if (action == Action.StormFeathers)
            {
                ResetFeathers();
            }

            return base.GetAction();
        }

        private void ResetFeathers()
        {
            featherTimer = stormSpeed;
        }
    }

    // Placeholder for spear attack
    private class SpearsClass : Attack
    {
        public SpearsClass() : base(AttackType.Spears)
        {
            
        }
    }

    // Normal variables
    private GameObject player;
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
    private float TEMPJABTIMER = 0.52f;
    private bool COUNTDOWNJAB = false;

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
        //jabCountdown = jabInterval;
        player = GameObject.FindGameObjectWithTag("Player");
        attackStack = new List<Attack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jabAnimator.gameObject.SetActive(false);

        // TEMPORARY
        jabCountdown = 1;
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
        var jabKey = KeyCode.Alpha1;
        var stormKey = KeyCode.Alpha2;
        var spearsKey = KeyCode.Alpha3;

        if (Input.GetKeyDown(jabKey))
        {
            JabAttack(2);
        }
        else if (Input.GetKeyDown(stormKey))
        {
            StormAttack();
        }
        else if (Input.GetKeyDown(spearsKey))
        {
            SpearsAttack();
        }

        if (jabCountdown > 0)
        {
            jabCountdown -= Time.deltaTime;
        }
        else
        {
            JabAttack(2);
            jabCountdown = jabInterval;
        }

        AttackUpdate();

        /*
        if (Input.GetKey(featherKey) == true)
        {

        }

        if (Input.GetKey(spearKey) == true)
        {

        }


        if (jabTimer > 0)
        {
            jabTimer -= Time.deltaTime;

            if (COUNTDOWNJAB == true)
            {
                TEMPJABTIMER -= Time.deltaTime;
            }

            if (TEMPJABTIMER <= 0)
            {
                Instantiate(javelin, javelinSpawnPoint.transform);
                COUNTDOWNJAB = false;
                TEMPJABTIMER = 0.52f;
            }
        }
        else if (Input.GetKey(jabKey) == true)
        {
            spriteRenderer.enabled = true;
            jabAnimator.gameObject.SetActive(false);

            if (jabCountdown <= 0)
            {
                jabCountdown = jabInterval;
                spriteRenderer.enabled = false;
                jabAnimator.gameObject.SetActive(true);
                jabTimer = jabMax;
                COUNTDOWNJAB = true;
            }

            jabCountdown -= Time.deltaTime;

            spriteRenderer.sprite = standardSprite;
        }*/
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

    private void AttackUpdate()
    {
        // Updates attacks in attack list
        if (attackStack.Count > 0)
        {
            attackStack[0].Update();

            Action firstAction = attackStack[0].GetAction();
            Action secondAction = Action.None;

            // Updates additional attack if it meets criterias
            if (attackStack.Count > 1)
            {
                switch (attackStack[0].GetAttackType())
                {
                    case AttackType.Jab:
                        if (attackStack[1].GetAttackType() == AttackType.Spears)
                        {
                            attackStack[1].Update();
                            secondAction = attackStack[1].GetAction();
                        }
                        break;

                    case AttackType.Storm:
                        if (attackStack[1].GetAttackType() == AttackType.Spears)
                        {
                            attackStack[1].Update();
                            secondAction = attackStack[1].GetAction();
                        }
                        break;

                    case AttackType.Spears:
                        if (attackStack[1].GetAttackType() == AttackType.Jab || attackStack[1].GetAttackType() == AttackType.Storm)
                        {
                            attackStack[1].Update();
                            secondAction = attackStack[1].GetAction();
                        }
                        break;
                }
            }

            // Perform actions
            AttackAction(firstAction);
            AttackAction(secondAction);

            // Remove in reverse order to prevent removing wrong element
            if (attackStack.Count > 1)
            {
                if (attackStack[1].End() == true)
                {
                    attackStack.RemoveAt(1);
                    Debug.Log("Second Attack Ended");
                }
            }

            if (attackStack[0].End() == true)
            {
                attackStack.RemoveAt(0);

                // Resets sprite in case there was an animation
                spriteRenderer.enabled = true;
                jabAnimator.gameObject.SetActive(false);
            }
        }
    }

    private void AttackAction(Action action)
    {
        switch (action)
        {
            case Action.JabTrack:
                float newY = Mathf.Sign(player.transform.position.y - transform.position.y) * Time.deltaTime * 2;
                // Checks that the difference is noticeable to avoid stuttering
                if (((player.transform.position.y - transform.position.y) * newY) > 0.005f)
                {
                    Vector3 newVector = new Vector3(0, newY, 0);
                    transform.Translate(newVector);
                }
                break;

            case Action.JabTarget:
                if (jabAnimator.gameObject.activeSelf == false)
                {
                    spriteRenderer.enabled = false;
                    jabAnimator.gameObject.SetActive(true);
                }
                break;

            case Action.JabStrike:
                Instantiate(javelin, javelinSpawnPoint.transform);
                break;
        }
    }

    private void JabAttack(float jabTimer)
    {
        JabClass jabAttack = new JabClass(jabTimer);

        attackStack.Add(jabAttack);
    }

    private void StormAttack()
    {
        StormClass stormAttack = new StormClass(4, 1);

        attackStack.Add(stormAttack);
    }

    private void SpearsAttack()
    {

    }
}
