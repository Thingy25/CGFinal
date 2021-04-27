Shader "Custom/ArrowShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_MainTexAlpha("Main Texture Alpha", Range(0,1)) = 0
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { 	"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "TransparentCutout"  
		}

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha : fade
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
		float _MainTexAlpha;
        sampler2D _MainTex;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 mainTex = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = mainTex * _Color;
            o.Alpha = _Color.a * _MainTexAlpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
