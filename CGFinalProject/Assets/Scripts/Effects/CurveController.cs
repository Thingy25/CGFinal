using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveController : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float effectDuration;
    [SerializeField] float curveValue;
    [SerializeField] float lightIntensity;
    [SerializeField] float vol;

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
        light.intensity = lightIntensity * curveValue;
        //Llamar audio.volume = vol * curveValue;
        psSize.size = curveValue;
        t += Time.deltaTime;
    }

   public void CurveEvaluation()
   {

   }
}
