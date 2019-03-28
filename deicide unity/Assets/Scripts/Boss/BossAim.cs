using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAim : MonoBehaviour
{
    bool PlayerAlive;
    GameObject player;

    private void Start()
    {
        PlayerAlive = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (PlayerAlive == true) 
        {
            BossPlayerLock();
        }
    }

    void BossPlayerLock()
    {
        if (player != null)
        {
            if (player.GetComponent<ShipMovement>().GetPlayerAlive() == true)
            {
                // Checking for ship position and aiming at it
                var PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                var aim = Quaternion.FromToRotation(Vector3.left, PlayerPos - transform.position);
                transform.rotation = aim;
                if (PlayerPos == null) { PlayerAlive = false; }
            }
        }
    }
}
