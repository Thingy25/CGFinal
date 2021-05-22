using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadersController : MonoBehaviour
{
    static ShadersController instance;
    [SerializeField] Renderer renderer, shieldRenderer;

    Color selectedColor;
    [SerializeField] [ColorUsage(true,true)] Color defaultColor, testColorsito;
    [SerializeField] float intensity;
    float factor;

    public static ShadersController Instance { get => instance; }
    public Color SelectedColor { get => selectedColor; }

    public delegate void UpdateColor();
    public event UpdateColor OnColorChanged;

    private void Awake() {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        factor = Mathf.Pow(2, intensity);
        selectedColor = defaultColor;
    }

    public void SetColorTest(Color colorsito) {
        selectedColor = colorsito;
        Debug.Log($"Color - R: {colorsito.r}, G: {colorsito.g}, B: {colorsito.b}");

        OnColorChanged();

        testColorsito = new Color(colorsito.r * factor, colorsito.g * factor, colorsito.b * factor, 100);

        renderer.material.SetColor("_MainTexColor", colorsito);
        renderer.material.SetColor("_MaskTexColor", colorsito);
        shieldRenderer.material.SetColor("_EdgesColor", colorsito);

        //Falta lo de la flecha
        //Falta lo de todos los sistemas de partículas que usen color

    }

    // public void SetCyanColor()
    // {
    //     color = cyan;

    //     renderer.material.SetColor("_MainTexColor", color);
    //     renderer.material.SetColor("_MaskTexColor", color);
    //     shieldRenderer.material.SetColor("_EdgesColor", color);

    //     //Línea para el material del shader de la flecha cuando se instancie
    //     //arrowRenderer.material.SetColor("_Color", color);
    //     //arrowRenderer.material.SetColor("_EdgesColor", color);
    // }

    // public void SetBlueColor()
    // {
    //     color = blue;

    //     renderer.material.SetColor("_MainTexColor", color);
    //     renderer.material.SetColor("_MaskTexColor", color);
    //     shieldRenderer.material.SetColor("_EdgesColor", color);

    //     //Línea para el material del shader de la flecha cuando se instancie
    //     //arrowRenderer.material.SetColor("_Color", color);
    //     //arrowRenderer.material.SetColor("_EdgesColor", color);
    // }

    // public void SetPurpleColor()
    // {
    //     color = purple;

    //     renderer.material.SetColor("_MainTexColor", color);
    //     renderer.material.SetColor("_MaskTexColor", color);
    //     shieldRenderer.material.SetColor("_EdgesColor", color);

    //     //Línea para el material del shader de la flecha cuando se instancie
    //     //arrowRenderer.material.SetColor("_Color", color);
    //     //arrowRenderer.material.SetColor("_EdgesColor", color);
    // }
}
