using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private void OnParticleSystemStopped() {

        /*Restrepo*/
        Debug.LogWarning("SI PASO POR ESTE CODIGO");
        EffectController.Instance?.ShieldActiveSound2();
        /*Restrepo*/

        UIController.Instance?.UnlockButton();
        gameObject.SetActive(false);

    }
}
