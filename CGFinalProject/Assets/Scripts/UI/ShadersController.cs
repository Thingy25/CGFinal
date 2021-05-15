using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadersController : MonoBehaviour
{
    [SerializeField] Renderer renderer, shieldRenderer;

    [SerializeField] [ColorUsage(true, true)] Color defaultColor, cyan, blue, purple;
    Color color, color2;

    private void Start()
    {
        SetDefaultColor();
    }

    public void SetDefaultColor()
    {
        color = defaultColor;

        renderer.material.SetColor("_MainTexColor", color);
        renderer.material.SetColor("_MaskTexColor", color);
        shieldRenderer.material.SetColor("_EdgesColor", color);
    }

    public void SetCyanColor()
    {
        color = cyan;

        renderer.material.SetColor("_MainTexColor", color);
        renderer.material.SetColor("_MaskTexColor", color);
        shieldRenderer.material.SetColor("_EdgesColor", color);
    }

    public void SetBlueColor()
    {
        color = blue;

        renderer.material.SetColor("_MainTexColor", color);
        renderer.material.SetColor("_MaskTexColor", color);
        shieldRenderer.material.SetColor("_EdgesColor", color);
    }

    public void SetPurpleColor()
    {
        color = purple;

        renderer.material.SetColor("_MainTexColor", color);
        renderer.material.SetColor("_MaskTexColor", color);
        shieldRenderer.material.SetColor("_EdgesColor", color);
    }
}
