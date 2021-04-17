Shader "Custom/Effect 1 shader"
{
    Properties
    {
        _Tex ("Main Texture", 2D) = "white" {}

		[HDR] _Color ("Texture Color", Color) = (1,1,1,1)
		[HDR] _EdgesColor ("Edges Color", Color) = (1,1,1,1)

		_SpeedX("Speed in X", Range(0,10)) = 2
		_SpeedY("Speed in Y", Range(0,10)) = 2

		_EmissionFactor ("Factor Emission Slider", Range(0,1)) = 0
		_SmoothnessEmission("Emission Smoothness", Range(0,0.5)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input
        {
            float2 uv_Tex;
			float3 worldNormal;
			float3 viewDir;
        };

		sampler2D _Tex;

        float4 _Color;
        float4 _EdgesColor;

		float _SpeedX;
		float _SpeedY;

		float _SmoothnessEmission;
		float _EmissionFactor;

		inline float3 CalculateTexture(sampler2D tex, float2 uvs)
		{
			float3 text = tex2D(tex, uvs).rgb;

			return text;
		}

		inline float3 CalculateEdges(Input IN)
		{
			float edges = 1 - dot(IN.viewDir, IN.worldNormal);

			return edges;
		}

		inline float2 MoveTextures(float2 uvs, float speedx, float speedy)
		{
			float2 movingUVs = uvs;
			float distanceX = speedx.x * _Time.y;
			float distanceY = speedy.x * _Time.y;

			movingUVs += float2(distanceX, distanceY);

			return movingUVs;
		}

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			half3 fresnelOutput = CalculateEdges(IN);

			float fresnelEmission = smoothstep(_SmoothnessEmission, 1 - _SmoothnessEmission, fresnelOutput + _EmissionFactor);

            o.Albedo = CalculateTexture(_Tex, MoveTextures(IN.uv_Tex, _SpeedX, _SpeedY)) /** (1 - fresnelOutput) + (fresnel * _Color)*/;
			o.Emission = fresnelEmission * _EdgesColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
