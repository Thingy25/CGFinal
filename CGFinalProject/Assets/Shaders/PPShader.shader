﻿Shader "Custom/PPShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Fac("Fac", Range(0,1)) = 0.0
    }
    SubShader
    {
      Pass{ 
        CGPROGRAM
        #pragma vertex vert_img
        #pragma fragment frag
        #pragma fragmentoption ARB_precision_hint_fastest
        #include "UnityCG.cginc"

        uniform sampler2D _MainTex;
        float _Fac;

        float4 frag(v2f_img i) : COLOR{
            /*float4 c1 = tex2D(_MainTex, i.uv);
            float4 c2 = c1 * 1.5;
            return c2;*/
            float4 main = tex2D(_MainTex, i.uv);
            float4 output = float4(0, 0, 0, 1);

            if (main.r > 0, main.g > 0, main.b > 0)
            {
                float gray = (main.r + main.g + main.b) / 3;
                output = float4(gray, gray, gray, 1);
            }

            output.r = output.r;
            output.g = output.g;
            output.b = output.b;

            return lerp(output, main, _Fac);
        }

        ENDCG
      }
       

    }
    FallBack off
    }
