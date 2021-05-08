using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadersController : MonoBehaviour
{
    [SerializeField] Renderer renderer;

    [SerializeField] [ColorUsage(true, true)] Color defaultColor;
    [SerializeField] [ColorUsage(true, true)] Color cyan;
    [SerializeField] [ColorUsage(true, true)] Color blue;
    [SerializeField] [ColorUsage(true, true)] Color green;
    [SerializeField] [ColorUsage(true, true)] Color purple;
    [SerializeField] [ColorUsage(true, true)] Color red;
    Color color;

    [SerializeField] [ColorUsage(true, true)] Color defaultColor2;
    [SerializeField] [ColorUsage(true, true)] Color cyan2;
    [SerializeField] [ColorUsage(true, true)] Color blue2;
    [SerializeField] [ColorUsage(true, true)] Color green2;
    [SerializeField] [ColorUsage(true, true)] Color purple2;
    [SerializeField] [ColorUsage(true, true)] Color red2;
    Color color2;

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
