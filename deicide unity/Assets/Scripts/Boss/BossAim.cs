﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAim : MonoBehaviour
{
    bool PlayerAlive;

    private void Start()
    {
        PlayerAlive = true;
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
        // Checking for ship position and aiming at it
        var PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        var aim = Quaternion.FromToRotation(Vector3.left, PlayerPos - transform.position);
        transform.rotation = aim;
        if (PlayerPos == null) { PlayerAlive = false; }
    }
}