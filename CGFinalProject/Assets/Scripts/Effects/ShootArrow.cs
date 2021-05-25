using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float arrowForce;
    [SerializeField] Transform shootPosition;

    Rigidbody rb;

    public void Shoot() {
        GameObject clone = Instantiate(arrowPrefab, shootPosition.position, arrowPrefab.transform.rotation);
        rb = clone.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * arrowForce * Time.deltaTime, ForceMode.Impulse);

        /*Restrepo*/
        AudioManager.Instance?.Play("Bow", EffectController.Instance.bowObj, EffectController.Instance.SpeedMultiplier, CurveController.Instance.audiovolume);
        //Cuando la animacion hace lanzar la flecha, suena el sonido [ Suena aqui debido a que si se pone antes suena antes de lanzar la flecha]
        /*Restrepo*/
    }
}
