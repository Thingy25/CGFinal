using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeSetter : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;
    [SerializeField] EffectTypes effect;

    private void Start() {
        switch (effect)
        {
            case EffectTypes.Shield:
            UIController.Instance.OnSizeChanged += SetShieldSize;
            break;
            case EffectTypes.Arrow:
            SetArrowSize();
            break;
        }   
    }

    void SetShieldSize() {
        foreach (ParticleSystem ps in particleSystems) {
            var main = ps.main;
            main.startSize = EffectController.Instance.SizeMultiplier;
        }
    }

    void SetArrowSize() {
        foreach (ParticleSystem ps in particleSystems) {
            var em = ps.emission;
            ParticleSystem.Burst mBurst = em.GetBurst(0);
            
            mBurst.count = Mathf.RoundToInt(mBurst.count.constant * EffectController.Instance.SizeMultiplier);
            em.SetBurst(0, mBurst);
            //em.burstCount = Mathf.RoundToInt(em.burstCount * EffectController.Instance.SizeMultiplier);
        }
    }
}
