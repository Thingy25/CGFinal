using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private void OnParticleSystemStopped() {
        UIController.Instance?.UnlockButton();
        gameObject.SetActive(false);
    }
}
