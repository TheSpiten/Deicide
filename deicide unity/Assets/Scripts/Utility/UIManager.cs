using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameObject baseUI;
    private bool showUI;

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
        baseUI = GameObject.Find("UI_base");
        showUI = true;

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
        if (showUI == true)
        {
            UpdateHealth();
            UpdateDash();
            UpdatePowerup();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("Alternate Movement");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
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

    public void TurnOffUI()
    {
        showUI = false;
        baseUI.SetActive(false);
        healthUI.SetActive(false);
        repairIcon.SetActive(false);
        shieldIcon.SetActive(false);
        dynamiteIcon.SetActive(false);
        playerDashIcon.SetActive(false);
        playerDashText.gameObject.SetActive(false);
    }
}
