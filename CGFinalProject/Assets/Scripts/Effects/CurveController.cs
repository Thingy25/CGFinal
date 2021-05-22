using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTypes { Shield, Arrow }
public class CurveController : MonoBehaviour
{
    static CurveController instance;

    [SerializeField] AnimationCurve curve1;
    //[SerializeField] AnimationCurve curve2;
    [SerializeField] float effectDuration;
    [SerializeField] float lightIntensity;
    [SerializeField] float vol;
    public float curve1Value;
    //public float curve2Value;

    //public EffectTypes effectType;

    //Light light;
    //ParticleSystem ps;

    [SerializeField] ParticleSystem ps;
    //[SerializeField] ParticleSystem ps2;
    //[SerializeField] ParticleSystem ps3;

    //ParticleSystem.SizeOverLifetimeModule psSize;
    ParticleSystem.LightsModule psLight;

    float t = 0;
    void Awake()
    {
        if (instance == null) { instance = this; }
        //ps = GetComponent<ParticleSystem>();
        //psSize = ps.sizeOverLifetime;
        //psLight = ps.lights;
        //psSize = ps.sizeOverLifetime;
        psLight = ps.lights;
        //ps2Size = ps2.sizeOverLifetime;
        //ps3Size = ps3.sizeOverLifetime;
    }

    void Update()
    {
        if(t>=effectDuration)
        {
            t = 0;
        }
        curve1Value = curve1.Evaluate(t / effectDuration);
        //curve2Value = curve2.Evaluate(t / effectDuration);
        //light.intensity = lightIntensity * curveValue;
        //Llamar audio.volume = vol * curveValue;
        //psSize.size = curve1Value;
        psLight.intensityMultiplier = curve1Value;
        t += Time.deltaTime;
    }

   public void EvaluateCurve()
   {
        if (t >= effectDuration)
        {
            t = 0;
        }
        curve1Value = curve1.Evaluate(t / effectDuration);
        //light.intensity = lightIntensity * curveValue;
        //Llamar audio.volume = vol * curveValue;
        //psSize.size = curve1Value;
        psLight.rangeMultiplier = curve1Value;
        t += Time.deltaTime;
    }
}
