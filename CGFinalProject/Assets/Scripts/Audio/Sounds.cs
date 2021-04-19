using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string nombre;

    public AudioClip clip;

    public float volume;
    public float pitch;

    [HideInInspector]
    public AudioSource fuente;
}
