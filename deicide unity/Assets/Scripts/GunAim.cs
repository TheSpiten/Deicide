using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    void Update()
    {
        // Checking for mouse position and making a Quaternion of it
        var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = 0;
        var aim = Quaternion.FromToRotation(Vector3.right, MousePos - transform.position);
        //if (aim.z > 0.5f)
        //    aim.z = 0.5f;
        //else if (aim.z < -0.5f)
        //    aim.z = -0.5f;
        if (aim.z <= 0.5f && aim.z >= -0.5f)
        {
            transform.rotation = aim;
        }
        //transform.rotation = aim;
    }
}
