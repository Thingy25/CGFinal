using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveController : MonoBehaviour
{
    static CurveController instance;

    [SerializeField] AnimationCurve curve;
    [SerializeField] float effectDuration;
    [SerializeField] float lightIntensity;
    [SerializeField] float vol;
    public float curveValue;

    //Light light;
    ParticleSystem ps;

    ParticleSystem.SizeOverLifetimeModule psSize;
    ParticleSystem.LightsModule psLight;

    float t = 0;
    void Awake()
    {
        if (instance == null) { instance = this; }
        ps = GetComponent<ParticleSystem>();
        psSize = ps.sizeOverLifetime;
        psLight = ps.lights;
    }

    // Update is called once per frame
    void Update()
    {
        curveValue = curve.Evaluate(t / effectDuration);
        //light.intensity = lightIntensity * curveValue;
        //Llamar audio.volume = vol * curveValue;
        psSize.size = curveValue;
        psLight.rangeMultiplier = curveValue;
        t += Time.deltaTime;
    }

   public void CurveEvaluation()
   {
        curveValue = curve.Evaluate(t / effectDuration);
        //light.intensity = lightIntensity * curveValue;
        //Llamar audio.volume = vol * curveValue;
        psSize.size = curveValue;
        psLight.rangeMultiplier = curveValue;
        t += Time.deltaTime;
    }
}
