using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossfunctions : MonoBehaviour
{
    // Boss state and general attack variables
    enum AttackType {Jab, Storm, Spears}
    enum JabState {Track, Target, Strike}
    enum RainState {Eye, End}
    enum BossPhase {Normal0, Enraged0}
    enum Action {None, JabTrack, JabTarget, JabStrike, StormWait, StormFeathers, SpearsRainLeft, SpearsRainRight, SpearsRainRandom}

    // Declares base attack class
    private class Attack
    {
        protected AttackType attackType;
        protected Action action;
        protected bool startAnimation;
        protected bool end;

        public Attack(AttackType type)
        {
            attackType = type;
            action = Action.None;
            startAnimation = true;
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
            jabTimer = trackTimer / 8;
            endTimer = trackTimer / 4;
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
            stormSpeed = 1 / speed;
            featherTimer = stormSpeed / 2;
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
    
    private class SpearsClass : Attack
    {
        private float eyeWait;
        private float endTimer;
        private float spearDuration;
        private float spearSpeed;
        private string rainType;
        private RainState state;
        private Action action;

        public SpearsClass(float speed, string type) : base(AttackType.Spears)
        {
            state = RainState.Eye;
            spearSpeed = speed;
            eyeWait = 1.0f;
            endTimer = 1.0f;
            action = Action.None;
            rainType = type;
        }

        public override void Update()
        {
            switch (state)
            {
                case RainState.Eye:
                    if (eyeWait > 0)
                    {
                        eyeWait -= Time.deltaTime;
                    }
                    else
                    {
                        switch (rainType)
                        {
                            case "Left":
                                action = Action.SpearsRainLeft;
                                break;

                            case "Right":
                                action = Action.SpearsRainRight;
                                break;

                            case "Random":
                                action = Action.SpearsRainRandom;
                                break;
                        }
                    }
                    break;

                case RainState.End:
                    if (endTimer > 0)
                    {
                        endTimer -= Time.deltaTime;
                    }
                    else
                    {
                        end = true;
                    }
                    break;
            }
        }

        public override Action GetAction()
        {
            Action returnAction = action;
            if (action == Action.SpearsRainLeft ||action == Action.SpearsRainRight || action == Action.SpearsRainRandom)
            {
                action = Action.None;
                state = RainState.End;
            }
            return returnAction;
        }
    }

    // Normal variables
    private GameObject player;
    public GameObject javelin;
    public GameObject javelinSpawnPoint;
    private float originalTransformX;
    public float jabInterval;
    private float jabCountdown;

    private SpriteRenderer spriteRenderer;
    public Sprite standardSprite;
    public Animator anticipationAnimator;
    public Animator jabAnimator;
    public Animator stormAnimator;
    public Animator spearsAnimator;

    private int bossPhase;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        originalTransformX = transform.position.x;
        attackStack = new List<Attack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anticipationAnimator.gameObject.SetActive(false);
        jabAnimator.gameObject.SetActive(false);
        stormAnimator.gameObject.SetActive(false);
        spearsAnimator.gameObject.SetActive(false);

        bossPhase = 0;

        // TEMPORARY
        jabCountdown = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().GameStarting() == false)
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
                StormAttack(4, 1);
            }
            else if (Input.GetKeyDown(spearsKey))
            {
                SpearsAttack("Left");
            }
            /*
            if (jabCountdown > 0)
            {
                jabCountdown -= Time.deltaTime;
            }
            else
            {
                JabAttack(2);
                jabCountdown = jabInterval;
            }
            */
            float health = GetComponent<BossHealth>().GetBossPercentage();
            if (health < 0.5f)
            {
                if (bossPhase != 3)
                {
                    bossPhase = 3;
                    attackStack.Clear();
                }

                if (attackStack.Count <= 0)
                {
                    // Phase 3 attacks

                }
            }
            else if (health < 0.8f)
            {
                if (bossPhase != 2)
                {
                    bossPhase = 2;
                    attackStack.Clear();
                }

                if (attackStack.Count <= 0)
                {
                    // Phase 2 attacks

                }
            }
            else
            {
                if (bossPhase != 1)
                {
                    bossPhase = 1;
                    attackStack.Clear();
                }

                if (attackStack.Count <= 0)
                {
                    // Phase 1 attacks
                    JabAttack(2);
                    SpearsAttack("Random");

                }
            }
            /*
            if (attackStack.Count <= 0)
            {
                JabAttack(2);
                SpearsAttack("Left");
                JabAttack(2);
                StormAttack(4, 1);
                JabAttack(2);
                SpearsAttack("Right");
                JabAttack(2);
                SpearsAttack("Random");
                JabAttack(2);
                StormAttack(4, 1.5f);
                JabAttack(2);
                StormAttack(4, 0.75f);
            }
            */
            AttackUpdate();
        }
        else
        {
            transform.position = new Vector2(transform.position.x - Time.deltaTime * 2.8f, transform.position.y);
            originalTransformX = transform.position.x;
            spriteRenderer.enabled = false;
            anticipationAnimator.gameObject.SetActive(true);
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
                    transform.position = new Vector3(originalTransformX, transform.position.y, transform.position.z);
                    attackStack.RemoveAt(1);
                }
            }

            if (attackStack[0].End() == true)
            {
                transform.position = new Vector3(originalTransformX, transform.position.y, transform.position.z);
                attackStack.RemoveAt(0);

                // Resets sprite in case there was an animation
                spriteRenderer.enabled = true;
                jabAnimator.gameObject.SetActive(false);
                stormAnimator.gameObject.SetActive(false);
                anticipationAnimator.gameObject.SetActive(false);
                anticipationAnimator.speed = 1;
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
                spriteRenderer.enabled = false;
                anticipationAnimator.gameObject.SetActive(true);
                break;

            case Action.JabTarget:
                if (jabAnimator.gameObject.activeSelf == false)
                {
                    anticipationAnimator.speed = 0;
                }
                spearsAnimator.gameObject.SetActive(false);
                break;

            case Action.JabStrike:
                Instantiate(javelin, javelinSpawnPoint.transform);
                transform.Translate(-2.5f, 0, 0);
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(2);
                jabAnimator.gameObject.SetActive(true);
                anticipationAnimator.gameObject.SetActive(false);
                break;

            case Action.StormWait:
                if (stormAnimator.gameObject.activeSelf == false)
                {
                    spriteRenderer.enabled = false;
                    stormAnimator.gameObject.SetActive(true);
                }
                break;

            case Action.StormFeathers:
                if (stormAnimator.gameObject.activeSelf == false)
                {
                    spriteRenderer.enabled = false;
                    stormAnimator.gameObject.SetActive(true);
                }
                GetComponent<FeatherStorm>().FeatherAttack();
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(5);
                break;

            case Action.SpearsRainLeft:
                GameObject.FindGameObjectWithTag("SpearSpawner").GetComponent<SpearSpawner>().SpearInstantiate("Left");
                spearsAnimator.gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(8);
                break;

            case Action.SpearsRainRight:
                GameObject.FindGameObjectWithTag("SpearSpawner").GetComponent<SpearSpawner>().SpearInstantiate("Right");
                spearsAnimator.gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(8);
                break;

            case Action.SpearsRainRandom:
                GameObject.FindGameObjectWithTag("SpearSpawner").GetComponent<SpearSpawner>().SpearInstantiate("Random");
                spearsAnimator.gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(8);
                break;
        }
    }

    private void JabAttack(float jabTimer)
    {
        JabClass jabAttack = new JabClass(jabTimer);

        attackStack.Add(jabAttack);
    }

    private void StormAttack(float duration, float speed)
    {
        StormClass stormAttack = new StormClass(duration, speed);

        attackStack.Add(stormAttack);
    }

    private void SpearsAttack(string type)
    {
        SpearsClass spearAttack = new SpearsClass(1, type);

        attackStack.Add(spearAttack);
    }
}
