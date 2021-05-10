using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] ParticleSystem[] systems;

    private void Start() {
        SetSpeed();
    }

    public void SetSpeed() {
        Debug.Log("Setting speed");
        foreach (ParticleSystem ps in systems) {
            var main = ps.main;
            main.simulationSpeed = EffectController.Instance.SpeedMultiplier;
        }
    }

    private void OnParticleSystemStopped() {
        UIController.Instance?.UnlockButton();
        Destroy(transform.parent.gameObject, 2f);
    }
}
