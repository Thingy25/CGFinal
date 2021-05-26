using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleColorSetter : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;

    private void Start() {
        ShadersController.Instance.OnColorChanged += ChangeColor;
    }

    void ChangeColor() {
        foreach (ParticleSystem ps in particleSystems) {
            var main = ps.main;
            main.startColor = ShadersController.Instance.SelectedColor;
        }
    }
}
