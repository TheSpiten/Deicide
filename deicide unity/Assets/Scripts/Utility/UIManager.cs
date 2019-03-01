using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject healthUI;
    private float healthValue;

    float playerDashTimer;
    private Text playerDashText;
    private GameObject playerDashIcon;

    enum Powerup { none, repair, dynamite, shield };
    Powerup playerPowerup;
    private GameObject repairIcon;
    private GameObject shieldIcon;
    private GameObject dynamiteIcon;
        
    void Awake()
    {
        healthUI = GameObject.Find("UI_health");
        playerPowerup = Powerup.none;
        playerDashTimer = -1;
        playerDashText = transform.Find("UI_dash_timer").GetComponent<Text>();
        playerDashIcon = transform.Find("UI_dash_ready").gameObject;

        repairIcon = transform.Find("UI_powerup_icons").transform.Find("UI_powerup_repair").gameObject;
        shieldIcon = transform.Find("UI_powerup_icons").transform.Find("UI_powerup_shield").gameObject;
        dynamiteIcon = transform.Find("UI_powerup_icons").transform.Find("UI_powerup_dynamite").gameObject;
    }

    private void Start()
    {
        SetPowerupsInactive();
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

        healthUI.transform.rotation = Quaternion.Euler(0, 0, -90 + healthValue * 0.9f);
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
            playerDashIcon.SetActive(true);
            playerDashText.gameObject.SetActive(false);
        }
        else
        {
            playerDashText.gameObject.SetActive(true);
            playerDashText.text = Mathf.Ceil(playerDashTimer).ToString();
            playerDashIcon.SetActive(false);
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
        repairIcon.SetActive(false);
        shieldIcon.SetActive(false);
        dynamiteIcon.SetActive(false);
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
