using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessingCam : MonoBehaviour
{
    static PostProcessingCam instance;
    public static PostProcessingCam Instance { get => instance; }

    public Shader shader;
    [Range(0, 1)]
    public float fac;
    private Material currentMat;

    Material mat
    {
        get
        {
            if (currentMat == null)
            {
                Debug.Log("Ccsmkzdlfsjl");
                currentMat = new Material(shader);
                currentMat.hideFlags = HideFlags.HideAndDontSave;
            }
            return currentMat;
        }
    }

    void Awake()
    {
        if (instance == null) instance = this;
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = true;
            return;
        }
        if (!shader || !shader.isSupported)
        {
            enabled = false;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetFloat("_Fac", fac);
        Graphics.Blit(source, destination, mat);
    }

    void Update()
    {
        
    }

    private void OnDisable()
    {
        if (currentMat)
        {
            DestroyImmediate(currentMat);
        }
    }

    //[SerializeField] Material postproMat;
    //private void OnRenderImage(RenderTexture source, RenderTexture destination)
    //{
    //    Graphics.Blit(source, destination, postproMat); 
    //}
}
