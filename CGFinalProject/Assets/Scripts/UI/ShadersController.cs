using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadersController : MonoBehaviour
{
    [SerializeField] Renderer renderer, shieldRenderer;

    Color color, color2;
    [SerializeField] [ColorUsage(true,true)] Color defaultColor, testColorsito;
    [SerializeField] float intensity;
    float factor;

    private void Start()
    {
        factor = Mathf.Pow(2, intensity);
    }

    public void SetColorTest(Color colorsito) {
        Debug.Log($"Color - R: {colorsito.r}, G: {colorsito.g}, B: {colorsito.b}");


        testColorsito = new Color(colorsito.r * factor, colorsito.g * factor, colorsito.b * factor, 100);
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
