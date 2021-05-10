using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    static EffectController instance;
    [SerializeField] GameObject shieldObj; //Escudo
    [SerializeField] GameObject arrowObj; //Flecha

    GameObject activeEffectObject;
    Renderer shieldRenderer;
    Animator anim;
    ParticleSystem ps;
    [SerializeField] List<ParticleSystem> pSystems = new List<ParticleSystem>();

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
        shieldRenderer = shieldObj.GetComponent<Renderer>();
        t = effectDuration;
        GetSubsystems();
    }

    private void Update() {
        SetAnimatorSpeed();
    }

    public void PlayEffect(int effect) {
        switch (effect)
        {
            case 0: //Escudo
            anim?.SetTrigger("ShieldAnimation");
            Invoke("GetAnimationTime", 0.2f);
            //To-Do: Cinemachine change camera
            break;
            case 1: //Flecha    
            anim?.SetTrigger("ArrowAnimation");
            Invoke("GetAnimationTime", 0.2f); 
            //To-Do: Cinemachine change camera       
            break;
        }
    }

    void Shield() {
        shieldObj.SetActive(true);
    }

    void Arrow() {
        GetComponent<ShootArrow>().Shoot(); //Agregar parámetro para speed multiplier
    }

    void GetSubsystems() {
        ParticleSystem[] systems = shieldObj.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem _ps in systems) {
            pSystems.Add(_ps);
        }
    }

    public void ModifyPSDuration() { //Revisar con Gio
        foreach (ParticleSystem _ps in pSystems) {
            var main = _ps.main;
            main.simulationSpeed = speedMultiplier;
        }
    }

    void GetAnimationTime() {
        AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);
        Debug.Log(animState.length);
        animLength = animState.length;
    }

    void SetAnimatorSpeed() {
        anim.speed = speedMultiplier;
        shieldRenderer.material.SetFloat("_TimeNumber", Mathf.Clamp(speedMultiplier, 0f, 1f));
    }
}
