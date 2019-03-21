using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int bossHealth = 1000;
    public float shakeMEUP = 0.0f;
    public GameObject Explosiooooooooons;

    private void FixedUpdate()
    {
        shakeMEUP += 0.1f;
    }

    public void DamageBoss(int damage)
    {
        bossHealth -= damage;
        if (bossHealth <= 0)
        {
            shakeMEUP = 0.0f;
            Camera.main.GetComponent<ScreenShake>().Shake(0.3f, 1.0f);
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomito = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-3.0f, 3.0f), 0);
                var expl = (GameObject)Instantiate(Explosiooooooooons, transform.position + randomito, transform.rotation);
            }
            Time.timeScale = 0.25f;
            Destroy(gameObject, 0.2f);
        }
    }

    public float GetBossHealth()
    {
        return bossHealth;
    }
}
