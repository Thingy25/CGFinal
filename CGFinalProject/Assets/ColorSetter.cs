using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;
    [SerializeField] Gradient mGradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    [SerializeField] EffectTypes effect;

    private void Start() {
        switch (effect)
        {
            case EffectTypes.Shield: 
            ShadersController.Instance.OnColorChanged += SetShieldColor;
            break;
            case EffectTypes.Arrow:
            SetArrowColor();
            break;
        }
        //SetGradient();       
    }

    void SetGradient(Gradient psGradient) {

        colorKey = new GradientColorKey[2];
        colorKey[0].color = ShadersController.Instance.SelectedColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = ShadersController.Instance.SelectedColor;
        colorKey[1].time = 1.0f;

        // alphaKey = new GradientAlphaKey[2];
        // alphaKey[0].alpha = 1.0f;
        // alphaKey[0].time = 0.0f;
        // alphaKey[1].alpha = 1.0f;
        // alphaKey[1].time = 1.0f;


        psGradient.SetKeys(colorKey, psGradient.alphaKeys);
    }

    public void SetShieldColor() {
        foreach (ParticleSystem ps in particleSystems) {
            var main = ps.main;
            main.startColor = ShadersController.Instance.SelectedColor; //mGradient;            
        }
    }

    public void SetArrowColor() {
        foreach (ParticleSystem ps in particleSystems) {
            var main = ps.main;
            main.startColor = ShadersController.Instance.SelectedColor; //mGradient;            
        }
        particleSystems[2].GetComponent<Renderer>().material.SetColor("_Color", ShadersController.Instance.SelectedColor);
        particleSystems[2].GetComponent<Renderer>().material.SetColor("_EdgesColor", ShadersController.Instance.SelectedColor);
    }
}
