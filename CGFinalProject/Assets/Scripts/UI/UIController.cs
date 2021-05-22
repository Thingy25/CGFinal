using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class UIController : MonoBehaviour
{
    static UIController instance;
    #region Variables
    [Header("Buttons and sliders")]
    [SerializeField] GameObject buttonPanel;
    [SerializeField] Slider speedSlider;
    [SerializeField] Slider sizeSlider;
    [SerializeField] Slider phSlider;
    [SerializeField] Button buttonEffect1;
    [SerializeField] Button buttonEffect2;
    [SerializeField] Button buttonActive;
    [SerializeField] TextMeshProUGUI statusText;

    [Header("Slider text")] [Space(20)]    
    [SerializeField] TextMeshProUGUI slider1Value;
    [SerializeField] TextMeshProUGUI slider2Value;
    [SerializeField] TextMeshProUGUI slider3Value;

    [SerializeField] CinemachineVirtualCamera arrowCam;

    int selectedEffect;
    bool isPlayingEffect;

    public static UIController Instance { get => instance; }
    #endregion

    private void Awake() {
        if (instance == null) instance = this;
    }

    private void Start() {
        statusText.text = "";
        DefaultValues();
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
            arrowCam.gameObject.SetActive(false);
            break;
            case 1:
            buttonEffect2.interactable = false;
            buttonEffect1.interactable = true;
            selectedEffect = effectIndex;
            statusText.text = "Efecto seleccionado: Flecha";
            arrowCam.gameObject.SetActive(true);
            break;            
            default: break;
        }

    }

    public void SetSpeed() {
        EffectController.Instance.SpeedMultiplier = speedSlider.value;
        EffectController.Instance.ModifyPSDuration();
        slider1Value.text = speedSlider.value.ToString("#.0");
    }

    public void SetSize() {
        //Implementar cambio de tamaño
        //Llamar alguna función que cambie el tamaño etc etc.
        slider2Value.text = sizeSlider.value.ToString("#.0");
    }

    public void SetThirdParameter() { //Placeholder name
        //Implementar lo que sea que haga el tercer efecto
        //Llamar alguna función que haga eso, etc
        slider3Value.text = phSlider.value.ToString("#.0");
    }
    

    public void LockButton() {
        buttonActive.interactable = false;
    }

    public void UnlockButton() {
        buttonActive.interactable = true;
        buttonPanel.SetActive(true);
    }

    void DefaultValues() {
        speedSlider.value = 1;
        sizeSlider.value = 1;
        phSlider.value = 1;
        slider1Value.text = speedSlider.value.ToString();
        slider2Value.text = sizeSlider.value.ToString();
        slider3Value.text = phSlider.value.ToString();
    }

    public void PlayEffect() { 
        EffectController.Instance.PlayEffect(selectedEffect);
        LockButton();
        buttonPanel.SetActive(false);
        statusText.text = "Activando efecto...";
    }
}
