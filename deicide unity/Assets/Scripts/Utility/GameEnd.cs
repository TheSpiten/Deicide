using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != true)
        {
            Invoke("NormalTime", 1.4f);
            Invoke("FadeOutVictoryScene", 0.5f);
        }

        if (GameObject.FindGameObjectWithTag("Player") != true)
        {
            Invoke("NormalTime", 1.1f);
            Invoke("FadeOutMainMenu", 0.3f);
        }
    }

    void NormalTime()
    {
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    void FadeOutMainMenu()
    {
        GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>().FadeToScene("MenuScene");
    }

    void VictoryScene()
    {
        SceneManager.LoadScene("Axel's Scene");
    }

    void FadeOutVictoryScene()
    {
        GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>().FadeToScene("Axel's Scene");
    }
}
