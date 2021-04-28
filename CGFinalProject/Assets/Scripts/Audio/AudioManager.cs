using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sonidos;
    GameObject Flecha;

    public void Awake()
    {
        foreach(Sounds s in sonidos)
        {
        }
    }

    public void Play(string _nombre, GameObject _gameObject)
    {             
        foreach (Sounds s in sonidos)
        {
            if (s.nombre == _nombre)
            {
                if( _gameObject.GetComponent<AudioSource>() == null ) {

                    s.fuente = _gameObject.AddComponent<AudioSource>();
                    s.fuente.clip = s.clip;
                    s.fuente.volume = s.volume;
                    s.fuente.pitch = s.pitch;
                    s.fuente.spatialBlend = 1;
                    s.fuente.Play();
                }
                else 
                    s.fuente.Play();
            }
        }

    }

    public void Stop(string _nombre, GameObject _gameObject)
    {
        foreach (Sounds s in sonidos)
        {
            if (s.nombre == _nombre)
            {
                s.fuente.Stop();
            }
        }

    }
}
