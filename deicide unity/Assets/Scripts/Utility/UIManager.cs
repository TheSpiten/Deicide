using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject healthUI;
    private float healthValue;

    enum Powerup { none, repair, dynamite, shield };
    Powerup playerPowerup;
        
    void Awake()
    {
        healthUI = GameObject.Find("UI_health");
        playerPowerup = Powerup.none;
    }
    
    void Update()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        healthValue = GetPlayerHealth();
        Debug.Log(healthValue);
        healthUI.transform.rotation = Quaternion.Euler(0, 0, -100 + healthValue);
    }

    private float GetPlayerHealth()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<ShipMovement>().health;
    }

    private void UpdatePowerup()
    {

    }
    /*
    private Powerup GetPlayerPowerup()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<ShipMovement>().Get
    }*/
}
