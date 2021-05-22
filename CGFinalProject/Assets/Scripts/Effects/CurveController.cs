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

    float t = 0;

    public static CurveController Instance { get => instance; }

    void Awake()
    {
        if (instance == null) { instance = this; }
        psLight = ps.lights;
    }

    void Update()
    {
        if(t>=effectDuration)
        {
            t = 0;
        }
        curve1Value = curve1.Evaluate(t / effectDuration);
        psLight.intensityMultiplier = curve1Value * intensityMultiplier;
        lightIntensity = psLight.intensityMultiplier;
        t += Time.deltaTime;
    }
}
