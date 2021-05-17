using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveController : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float effectDuration;
    [SerializeField] float curveValue;
    [SerializeField] float lightIntensity;

    [SerializeField] Light light;
    [SerializeField] ParticleSystem ps;

    ParticleSystem.SizeOverLifetimeModule psSize;

    float t = 0;
    void Start()
    {
        psSize = ps.sizeOverLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        curveValue = curve.Evaluate(t / effectDuration);
        t += Time.deltaTime;
    }
}
