using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorial : MonoBehaviour
{
    public GameObject tutorial;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("startTutorial", 1.0f);
    }

    void startTutorial()
    {
        tutorial.SetActive(true);
    }
}
