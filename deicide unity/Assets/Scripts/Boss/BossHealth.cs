using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int bossHealth = 1000;

    public void DamageBoss(int damage)
    {
        bossHealth -= damage;
        if (bossHealth <= 0)
            Destroy(gameObject);
    }
}
