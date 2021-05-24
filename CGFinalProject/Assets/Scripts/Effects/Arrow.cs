using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject particleSystemObj;
    [SerializeField] GameObject audiosource;

    private void OnCollisionEnter(Collision other) {
        ContactPoint contact = other.GetContact(0);
        Instantiate(particleSystemObj, contact.point + Vector3.forward * -0.5f, particleSystemObj.transform.rotation);

        /*Restrepo*/
        //Instantiate(audiosource, contact.point + Vector3.forward * -0.5f, audiosource.transform.rotation);
        AudioManager.Instance?.Play("ExploArrow", Instantiate(new GameObject(), contact.point + Vector3.forward * -0.5f, particleSystemObj.transform.rotation), 1f, 1f);

        //Cuando impacta la flecha se reproduce un Audio llamado "Arrow Impact" del AudioManager
        /*Restrepo*/

        Destroy(gameObject);
    }
}
