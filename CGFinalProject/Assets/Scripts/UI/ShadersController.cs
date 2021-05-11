using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadersController : MonoBehaviour
{
    [SerializeField] Renderer renderer, shieldRenderer;

    [SerializeField] [ColorUsage(true, true)] Color defaultColor, cyan, blue, green, purple, red;
    Color color, color2;

    public void SetDefaultColor()
    {
        color = defaultColor;
        color2 = defaultColor;

        renderer.material.SetColor("_MainTexColor", color);
        renderer.material.SetColor("_MaskTexColor", color2);
    }

    public void SetCyanColor()
    {
        color = cyan;
        color2 = cyan;

        renderer.material.SetColor("_MainTexColor", color);
        renderer.material.SetColor("_MaskTexColor", color2);
    }

    public void SetBlueColor()
    {
        color = blue;
        color2 = blue;

        renderer.material.SetColor("_MainTexColor", color);
        renderer.material.SetColor("_MaskTexColor", color2);
    }

    public void SetGreenColor()
    {
        color = green;
        color2 = green;

        renderer.material.SetColor("_MainTexColor", color);
        renderer.material.SetColor("_MaskTexColor", color2);
    }

    public void SetPurpleColor()
    {
        color = purple;
        color2 = purple;

        renderer.material.SetColor("_MainTexColor", color);
        renderer.material.SetColor("_MaskTexColor", color2);
    }

    public void SetRedColor()
    {
        color = red;
        color2 = red;

        renderer.material.SetColor("_MainTexColor", color);
        renderer.material.SetColor("_MaskTexColor", color2);
    }
}
