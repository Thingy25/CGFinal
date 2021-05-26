using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTypes { Shield, Arrow }
public class CurveController : MonoBehaviour
{
    static CurveController instance;

    [SerializeField] AnimationCurve curve1;
    [SerializeField] float effectDuration;
    [SerializeField] float lightIntensity;
    [SerializeField] float intensityMultiplier;
    public float curve1Value;

    [SerializeField] ParticleSystem ps;
    ParticleSystem.LightsModule psLight;

    public float audiovolume;

    float t = 0;

    public static CurveController Instance { get => instance; }

    void Awake()
    {
        //audiovolume = curve1.Evaluate(0f);
        audiovolume = 1f;
        if (instance == null) { instance = this; }
        psLight = ps.lights;
    }

    void Update()
    {
        if(t>=effectDuration)
        {
            t = 0f;
        }
        curve1Value = curve1.Evaluate(t / effectDuration);
        audiovolume = curve1Value;
        lightIntensity = psLight.intensityMultiplier;
        t += Time.deltaTime;
    }
}
