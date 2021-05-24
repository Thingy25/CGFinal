using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    public Sounds[] sonidos;
    public static AudioManager Instance { get => instance; }

        float curveValue;
    float effectDuration = 8;
    float t;

    public void Awake()
    {
        if (instance == null) instance = this;        
    }

    /*public void Play(string _nombre, GameObject _gameObject, float _pitchMod)
    {             
        foreach (Sounds s in sonidos)
        {
            if (sonidos[i].nombre == _nombre)
            {
                if( _gameObject.GetComponent<AudioSource>() == null ) {

                    s.fuente = _gameObject.AddComponent<AudioSource>();
                    s.fuente.clip = s.clip;
                    s.fuente.volume = s.volume;
                    s.fuente.pitch = _pitchMod;
                    s.fuente.spatialBlend = 1;
                    s.fuente.playOnAwake = true;
                    s.fuente.Play();
                }
                else 
                    s.fuente.Play();
            }
        }

    }*/

    public void Play(string _nombre, GameObject _gameObject, float _pitchMod, float _volumen)
    {
        for (int i = 0; i < sonidos.Length; i++)
        {
            if (sonidos[i].nombre == _nombre)
            {
                if (_gameObject.GetComponent<AudioSource>() == null)
                {
                    sonidos[i].fuente = _gameObject.AddComponent<AudioSource>();
                    sonidos[i].fuente.clip = sonidos[i].clip;
                    sonidos[i].fuente.volume = _volumen;
                    sonidos[i].fuente.pitch = _pitchMod;
                    sonidos[i].fuente.spatialBlend = 1;
                    sonidos[i].fuente.playOnAwake = true;
                    sonidos[i].fuente.Play();
                }
                else
                { 
                    sonidos[i].fuente.clip = sonidos[i].clip;
                    sonidos[i].fuente.volume = _volumen;
                    sonidos[i].fuente.pitch = _pitchMod;
                    sonidos[i].fuente.spatialBlend = 1;
                    sonidos[i].fuente.playOnAwake = true;
                    sonidos[i].fuente.Play();
                }
            }
        }

    }

    public void Stop(string _nombre, GameObject _gameObject)
    {
        for(int i = 0; i < sonidos.Length; i++)
        {
            if (sonidos[i].nombre == _nombre)
            {
                sonidos[i].fuente.Stop();
            }
        }

    }
}
