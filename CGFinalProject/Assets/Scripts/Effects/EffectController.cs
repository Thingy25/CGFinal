using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject psObj1;
    [SerializeField] GameObject psObj2;

    GameObject activeEffectObject;

    ParticleSystem ps;

    float t;
    float effectDuration;
    bool isActive;
    

    void Start() {  
        anim = null; //Placeholder
        if (!activeEffectObject) activeEffectObject = psObj1;  
        ps = activeEffectObject?.GetComponent<ParticleSystem>();
        effectDuration = ps.main.duration + ps.main.startLifetimeMultiplier;
        t = effectDuration;
    }

    void ResetValues() {
        ps = activeEffectObject?.GetComponent<ParticleSystem>();
        effectDuration = ps.main.duration + ps.main.startLifetimeMultiplier;
        t = effectDuration;
    } 

    public void SetActiveEffect(int effectIndex) {
        switch (effectIndex)
        {
            case 0:
            activeEffectObject = psObj1; break;
            case 1:             
            activeEffectObject = psObj2; break;
        }
        ResetValues();
    }

    void Update() {

        if (t >= effectDuration) {
            if (Input.GetButtonDown("Fire1")) {
                anim?.SetBool("Animation", true);
                activeEffectObject?.SetActive(true);
                t = 0;
                FindObjectOfType<AudioManager>().Play("Hola", this.gameObject);
            }
            if (activeEffectObject.activeSelf && t > effectDuration) {
                activeEffectObject?.SetActive(true);
                anim?.SetBool("Animation", false);
            }
        }

        t += Time.deltaTime;        
    }
}
