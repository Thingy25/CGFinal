using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    static EffectController instance;
    [SerializeField] GameObject shieldObj; //Escudo
    [SerializeField] GameObject arrowObj; //Flecha

    GameObject activeEffectObject;
    Animator anim;
    ParticleSystem ps;
    [SerializeField] List<ParticleSystem> subsystems = new List<ParticleSystem>();

    float t;
    float animLength;
    float effectDuration; 
    float speedMultiplier = 1f;
    bool isActive;

    public static EffectController Instance { get => instance;}
    public float SpeedMultiplier { get => speedMultiplier; set => speedMultiplier = value; }

    private void Awake() {
        if (instance == null) instance = this;
    }

    void Start() {  
        anim = GetComponent<Animator>();
        if (!activeEffectObject) activeEffectObject = shieldObj;  
        ps = activeEffectObject?.GetComponent<ParticleSystem>();
        effectDuration = ps.main.duration + ps.main.startLifetimeMultiplier;
        t = effectDuration;
        GetSubsystems();
    }

    public void PlayEffect(int effect) {
        switch (effect)
        {
            case 0: //Escudo
            anim?.SetTrigger("ShieldAnimation");
            Invoke("GetAnimationTime", 0.2f);
            //anim?.SetBool("ShieldAnimation", true);
            break;
            case 1: //Flecha            
            //anim?.SetBool("ArrowAnimation", true);
            break;
        }
    }

    void Shield() {
        shieldObj.SetActive(true);
    }

    void GetSubsystems() {
        ParticleSystem[] systems = shieldObj.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem _ps in systems) {
            subsystems.Add(_ps);
        }
    }

    public void ModifyPSDuration() { //Revisar con Gio
        foreach (ParticleSystem _ps in subsystems) {
            var main = _ps.main;
            float oldDuration = main.duration;
            main.duration *= speedMultiplier;
            oldDuration = main.duration;
        }
    }

    void GetAnimationTime() {
        //AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo animState = anim.GetNextAnimatorStateInfo(0);
        Debug.Log(animState.length);
        animLength = animState.length;
    }
}
