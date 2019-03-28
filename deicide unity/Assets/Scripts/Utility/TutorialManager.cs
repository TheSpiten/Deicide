using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject Tutorial;
    public GameObject TutorialUI;

    public bool TutorialIsActive = false;
    public bool TutorialUIIsActive = false;



    void FixedUpdate()
    {
        if (TutorialIsActive == true && Input.GetKeyDown(KeyCode.Space))
        {
            setTutorialUIActive();
        }

        if (TutorialUIIsActive == true && Input.GetKeyDown(KeyCode.Space))
        {
            TutorialUI.SetActive(false);
            Time.timeScale = 1.0f;
        }
        
    }

    public void setTutorialActive()
    {
        Time.timeScale = 0.0f;
        TutorialIsActive = true;
        Tutorial.SetActive(true);
    }

    public void setTutorialUIActive()
    {
        Tutorial.SetActive(false);
        TutorialUIIsActive = true;
        TutorialUI.SetActive(true);
    }
}
