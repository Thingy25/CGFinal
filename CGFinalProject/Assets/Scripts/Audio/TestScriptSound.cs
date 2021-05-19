using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptSound : MonoBehaviour
{
    //public static Action<String> Sound;
    public ParticleSystem Particulas;

    [SerializeField] float t2;

    [SerializeField] GameObject o1;
    [SerializeField] GameObject o2;
    [SerializeField] GameObject o3;


    private float t = 100;
    float state = 0;

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R) && state == 0)
        {
            Particulas.Play();
            FindObjectOfType<AudioManager>().Play("Escudo Activar", o1,1);
            t = 0;
            state++;
        }

        if(t > 0.9 && state == 1)
        {
            FindObjectOfType<AudioManager>().Play("Escudo Loop", o2,1);
            state++;
        }

        if(t > t2 && state == 2)
        {
            FindObjectOfType<AudioManager>().Stop("Escudo Loop", o2);
            FindObjectOfType<AudioManager>().Play("Escudo Final", o3,1);
            state++;
        }
        if (state != 0) t++;*/
    }
}
