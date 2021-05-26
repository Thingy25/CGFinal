using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject particleSystemObj;
    [SerializeField] GameObject gameObject1;

    private void OnCollisionEnter(Collision other) {
        ContactPoint contact = other.GetContact(0);
        Instantiate(particleSystemObj, contact.point + Vector3.forward * -0.5f, particleSystemObj.transform.rotation);

        /*Restrepo*/
        //Instantiate(audiosource, contact.point + Vector3.forward * -0.5f, audiosource.transform.rotation);
        AudioManager.Instance?.Play("ExploArrow", Instantiate(gameObject1), EffectController.Instance.SpeedMultiplier, CurveController.Instance.audiovolume);
        //Instantiate(new GameObject("Sonido"), contact.point + Vector3.forward * -0.5f, particleSystemObj.transform.rotation)
        //Cuando impacta la flecha se reproduce un Audio llamado "Arrow Impact" del AudioManager
        /*Restrepo*/

        Destroy(gameObject);
    }
}
