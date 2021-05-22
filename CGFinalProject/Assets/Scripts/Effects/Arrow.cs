﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject particleSystemObj;

    private void OnCollisionEnter(Collision other) {
        ContactPoint contact = other.GetContact(0);
        Instantiate(particleSystemObj, contact.point + Vector3.forward * -0.5f, particleSystemObj.transform.rotation);

        /*Restrepo*/
        //AudioManager.Instance?.Play("Arrow Impact", particleSystemObj, 1);
        //Cuando impacta la flecha se reproduce un Audio llamado "Arrow Impact" del AudioManager
        /*Restrepo*/

        Destroy(gameObject);
    }
}
