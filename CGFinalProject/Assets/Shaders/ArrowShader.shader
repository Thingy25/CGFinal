Shader "Custom/ArrowShader"
{
    Properties
    {
        [HDR] _Color ("Color", Color) = (1,1,1,1)
        [HDR] _EdgesColor ("Edges Color", Color) = (1,1,1,1)
		_MainTexAlpha("Main Texture Alpha", Range(0,1)) = 0
		_EdgeStrength ("Edge Strength", Float) = 1
		_EdgeIntensity ("Edge Intensity", Float) = 1
    }
    SubShader
    {
        Tags { 	"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "TransparentCutout"  
		}
		Cull Off

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha : fade
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
			float4 screenPos;
			float eyeDepth;
        };

        fixed4 _Color;
        fixed4 _EdgesColor;
		float _MainTexAlpha;
		sampler2D_float _CameraDepthTexture;
		float _EdgeStrength;
		float _EdgeIntensity;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos)));
			float4 edges = (_EdgeStrength * (depth - IN.screenPos.w));
			edges = saturate(edges);
			edges = 1 - edges;
			edges *= _EdgeIntensity;

			o.Albedo = _Color;
            o.Emission = edges * _EdgesColor;
            o.Alpha = _Color.a * _MainTexAlpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
