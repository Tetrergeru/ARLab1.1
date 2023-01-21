using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    public Victorine Victorine;

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Victorine.Answer(true);
        }
        else if (Input.GetKey(KeyCode.RightShift))
        {
            Victorine.Answer(false);
        }

    }
}
