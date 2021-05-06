using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    static UIController instance;
    [SerializeField] Slider speedSlider;
    [SerializeField] Button buttonEffect1;
    [SerializeField] Button buttonEffect2;
    [SerializeField] Button buttonActive;
    [SerializeField] TextMeshProUGUI statusText;

    int selectedEffect;
    bool isPlayingEffect;

    public static UIController Instance { get => instance; }

    private void Awake() {
        if (instance == null) instance = this;
    }

    private void Start() {
        statusText.text = "";
    }

    public void SelectEffect(int effectIndex)  { //0: Escudo, 1: Flecha
        switch (effectIndex)
        {
            //Seleccioné el efecto 1, por ende se desactiva el botón 1
            case 0:
            buttonEffect1.interactable = false;
            buttonEffect2.interactable = true;
            selectedEffect = effectIndex;
            statusText.text = "Efecto seleccionado: Escudo";
            break;
            case 1:
            buttonEffect2.interactable = false;
            buttonEffect1.interactable = true;
            selectedEffect = effectIndex;
            statusText.text = "Efecto seleccionado: Flecha";
            break;            
            default: break;
        }

    }

    public void SetSpeed() {
        EffectController.Instance.SpeedMultiplier = speedSlider.value;
        EffectController.Instance.ModifyPSDuration();
    }
    

    public void LockButton() {
        buttonActive.interactable = false;
    }

    public void UnlockButton() {
        buttonActive.interactable = true;

    }

    public void PlayEffect() { 
        EffectController.Instance.PlayEffect(selectedEffect);
        LockButton();
        statusText.text = "Activando efecto...";
    }
}
