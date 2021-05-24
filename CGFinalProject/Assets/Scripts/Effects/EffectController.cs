using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    static EffectController instance;
    [SerializeField] GameObject shieldObj; //Escudo
    [SerializeField] GameObject arrowObj; //Flecha

    [SerializeField] ParticleSystem arrowRings; //Rings particles

    GameObject activeEffectObject;
    Renderer shieldRenderer;
    Animator anim;
    ParticleSystem ps;
    [SerializeField] List<ParticleSystem> pSystems = new List<ParticleSystem>();

    [Header("Source")]
    [SerializeField] public GameObject shieldObje; // Para el Audio
    [SerializeField] public GameObject shieldObje2; // Para el Audio
    [SerializeField] public GameObject shieldObje3; // Para el Audio
    [SerializeField] public GameObject bowObj;

    float t;
    float animLength;
    float effectDuration;
    float speedMultiplier = 1f, sizeMultiplier = 1f;
    bool isActive;

    AudioSource audiosource;
    public bool shieldIsActive;

    public static EffectController Instance { get => instance;}
    public float SpeedMultiplier { get => speedMultiplier; set => speedMultiplier = value; }
    public float SizeMultiplier { get => sizeMultiplier; set => sizeMultiplier = value; }

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

        //Restrepito
        if (shieldIsActive)
        {
            AudioManager.Instance?.Play("Escudo Loop", shieldObje3, speedMultiplier, CurveController.Instance.audiovolume);
        }
        //Si el Escudo esta activo se reproduce el sonido.
        //Restrepito
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
            arrowRings.Play();
            //To-Do: Cinemachine change camera       
                break;
        }
    }

    void ShieldActiveSound() //Restrepo
    {
        AudioManager.Instance?.Play("Escudo Activar", shieldObje, speedMultiplier, CurveController.Instance.audiovolume);
        shieldIsActive = true;
    }//Se Reproduce el sonido del escudo del AudioManager,se activa un Bool para tener encuenta el Loop

    public void ShieldActiveSound2() //Restrepo
    {
        AudioManager.Instance?.Play("Escudo FInal", shieldObje2, speedMultiplier, CurveController.Instance.audiovolume);
        AudioManager.Instance?.Stop("Escudo Loop", shieldObje3);
        shieldIsActive = false;
    }//Se Reproduce el sonido del escudo del AudioManager,se Desactiva un Bool para tener encuenta el Loop y se desactiva el sonido del loop

    public void ExplosionActive(GameObject _gameObject)
    {
        AudioManager.Instance?.Play("ExploArrow", _gameObject , speedMultiplier, 1f);
    }


    void Shield() {
        shieldObj.SetActive(true);
        Invoke("ShieldActiveSound", 0f ); //Restrepo
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
        shieldRenderer.material.SetFloat("_TimeNumber", Mathf.Clamp(speedMultiplier, 1f, 5f));
    }
}
