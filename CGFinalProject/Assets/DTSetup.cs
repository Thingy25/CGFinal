using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DTSetup : MonoBehaviour
{
    [SerializeField] DOTweenAnimation[] tweens;
    float[] initialDurations;
    
    private void OnEnable() {
        foreach (DOTweenAnimation tw in tweens) {
            tw.duration = EffectController.Instance.SpeedMultiplier;
            tw.DOPlay();
        }
        GetComponent<Projector>().material.SetColor("_Color", ShadersController.Instance.SelectedColor);

    }

    public void RestartTweens() {
        foreach (DOTweenAnimation tw in tweens) {
            tw.DORestart();
        }
    }
    


}
