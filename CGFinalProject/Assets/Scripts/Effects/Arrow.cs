using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject particleSystemObj;

    private void OnCollisionEnter(Collision other) {
        Debug.Log("Activating ps");
        //particleSystemObj.SetActive(true); //Check how the particle system will work and change this accordingly
    }
}
