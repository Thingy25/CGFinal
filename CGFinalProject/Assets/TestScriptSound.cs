using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptSound : MonoBehaviour
{
    //public static Action<String> Sound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<AudioManager>().Play("Hola",this.gameObject);
            //Debug.LogError("R SI FUNCIONA OBVIO");
            //Sound?.Invoke("Hola");
        }
    }
}
