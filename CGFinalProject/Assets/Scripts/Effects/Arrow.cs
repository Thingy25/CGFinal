using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject particleSystemObj;

    private void OnCollisionEnter(Collision other) {
        Instantiate(particleSystemObj, other.transform.position, particleSystemObj.transform.rotation);
        Destroy(gameObject);
    }
}
