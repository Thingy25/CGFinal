using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string nombre;

    public AudioClip clip;

    [HideInInspector]
    public AudioSource fuente;
}
