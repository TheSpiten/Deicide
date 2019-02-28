using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject healthUI;
    private float healthValue;

    enum Powerup { none, repair, dynamite, shield };
    Powerup playerPowerup;

    float playerDashTimer;
    private Text playerDashText;
    private Text playerDashOKText;
        
    void Awake()
    {
        healthUI = GameObject.Find("UI_health");
        playerPowerup = Powerup.none;
        playerDashTimer = -1;
    }

    private void Start()
    {
        SetPowerupsInactive();

        playerDashText = transform.Find("UI_dash_timer").GetComponent<Text>();
        playerDashOKText = transform.Find("UI_dash_ok").GetComponent<Text>();
    }

    void Update()
    {
        UpdateHealth();
        UpdateDash();
        UpdatePowerup();
    }

    private void UpdateHealth()
    {
        healthValue = GetPlayerHealth();

        healthUI.transform.rotation = Quaternion.Euler(0, 0, -100 + healthValue);
    }

    private float GetPlayerHealth()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<ShipMovement>().GetPlayerHealth();
    }

    private void UpdateDash()
    {
        playerDashTimer = GetPlayerDashTimer();

        if (playerDashTimer <= 0)
        {
            playerDashOKText.gameObject.SetActive(true);
            playerDashText.gameObject.SetActive(false);
        }
        else
        {
            playerDashText.gameObject.SetActive(true);
            playerDashText.text = Mathf.Ceil(playerDashTimer).ToString();
            playerDashOKText.gameObject.SetActive(false);
        }
    }

    private float GetPlayerDashTimer()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<ShipMovement>().GetPlayerDashTimer();
    }

    private void UpdatePowerup()
    {
        SetPowerupsInactive();

        playerPowerup = GetPlayerPowerup();

        SetPowerupActive(playerPowerup);
    }
    
    private Powerup GetPlayerPowerup()
    {
        switch (GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUp>().GetPlayerPowerup())
        {
            case "repair":
                return Powerup.repair;

            case "shield":
                return Powerup.shield;

            case "dynamite":
                return Powerup.dynamite;

            default:
                return Powerup.none;
        }
    }

    private void SetPowerupsInactive()
    {
        transform.Find("UI_powerup_icons").transform.Find("UI_powerup_repair").gameObject.SetActive(false);
        transform.Find("UI_powerup_icons").transform.Find("UI_powerup_shield").gameObject.SetActive(false);
        transform.Find("UI_powerup_icons").transform.Find("UI_powerup_dynamite").gameObject.SetActive(false);
    }

    private void SetPowerupActive(Powerup powerup)
    {
        switch (powerup)
        {
            case Powerup.repair:
                transform.Find("UI_powerup_icons").transform.Find("UI_powerup_repair").gameObject.SetActive(true);
                break;

            case Powerup.shield:
                transform.Find("UI_powerup_icons").transform.Find("UI_powerup_shield").gameObject.SetActive(true);
                break;

            case Powerup.dynamite:
                transform.Find("UI_powerup_icons").transform.Find("UI_powerup_dynamite").gameObject.SetActive(true);
                break;

            default:
                break;
        }
    }
}
