using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float arrowForce;
    [SerializeField] Transform shootPosition;

    float cooldown = 5f;
    float t = 0;

    Rigidbody rb;

    public void Shoot() {
        GameObject clone = Instantiate(arrowPrefab, shootPosition.position, arrowPrefab.transform.rotation);
        rb = clone.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * arrowForce * Time.deltaTime, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Space) && t > cooldown) {
            Shoot(); //Placeholder, replace with UI button
            t = 0; 
        } 
        t+= Time.deltaTime;
    }
}
