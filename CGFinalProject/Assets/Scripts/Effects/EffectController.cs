using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    static EffectController instance;
    [SerializeField] GameObject shieldObj; //Escudo
    [SerializeField] GameObject arrowProjector;
    public GameObject shieldProjector;


    [SerializeField] ParticleSystem arrowRings; //Rings particles
    [SerializeField] GameObject shieldAnt; //Shield Anticipation


    GameObject activeEffectObject;
    Renderer shieldRenderer;
    Animator anim;
    ParticleSystem ps;
    //[SerializeField] List<ParticleSystem> pSystems = new List<ParticleSystem>();
    [SerializeField] ParticleSystem[] pSystems;

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
            bowObj.SetActive(false);
            anim?.SetTrigger("ShieldAnimation");
            shieldAnt.SetActive(true);
            Invoke("GetAnimationTime", 0.2f);            
            break;

            case 1: //Flecha
            bowObj.SetActive(true);
            arrowProjector.SetActive(true);    
            anim?.SetTrigger("ArrowAnimation");
            Invoke("GetAnimationTime", 0.2f);
            arrowRings.Play();   
            break;
        }
        PostProcessingCam.Instance.fac = Mathf.Lerp(0, 1, 1f);
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
        AudioManager.Instance?.Play("ExploArrow", _gameObject , speedMultiplier, CurveController.Instance.audiovolume);
    }


    void Shield() {
        shieldObj.SetActive(true);        
        shieldProjector.transform.position += new Vector3(0, 4.5f * sizeMultiplier, 0);
        shieldProjector.SetActive(true);
        Invoke("ShieldActiveSound", 0f ); //Restrepo
    }

    public void ShieldEnd() {
        shieldProjector.SetActive(false);    
        shieldProjector.transform.position -= new Vector3(0, 4.5f * sizeMultiplier, 0);
        shieldAnt.SetActive(false);
    }

    void Arrow() {
        GetComponent<ShootArrow>().Shoot(); //Agregar parámetro para speed multiplier
        AudioManager.Instance?.Play("Bow", bowObj, SpeedMultiplier, 1f); //NO SE PUEDE MODIFICAR EL VOLUMEN POR RAMIREZ QUE PUSO EL CURVE CONTROLER DENTRO DE UN OBJETO QUE NO ESTABA NI CREADO !
        arrowProjector.SetActive(false);
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
