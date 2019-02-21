using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAim : MonoBehaviour
{
    void Update()
    {
        // Checking for ship position and aiming at it
        var PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        var aim = Quaternion.FromToRotation(Vector3.left, PlayerPos - transform.position);
        transform.rotation = aim;
    }
}
