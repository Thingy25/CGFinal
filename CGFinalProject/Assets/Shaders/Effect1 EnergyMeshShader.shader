Shader "Custom/Shield/Mesh"
{
    Properties
    {
        [HDR] _MainColor ("Main Texture Color", Color) = (1,1,1,1)
		[HDR] _EdgesColor("Edges Color", Color) = (1,1,1,1)

		_EmissionFactor("Factor Emission Slider", Range(-1,1)) = 0
		_SmoothnessEmission("Emission Smoothness", Range(-0.5,0.5)) = 0

		_MeshAlpha("Main Texture Alpha", Range(0,1)) = 0

        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { 
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "TransparentCutout" 
		}
        //LOD 200
        Cull Off //Para que se vea por ambos lados

		pass {
			ZWrite On
			ColorMask 0
		}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha : fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
			float3 worldNormal;
			float3 worldPos;
			float3 viewDir;
        };
		
		fixed4 _MainColor;
		fixed4 _EdgesColor;

		float _SmoothnessEmission;
		float _EmissionFactor;

		float _MeshAlpha;

        half _Glossiness;
        half _Metallic;

		inline float3 CalculateEdges(Input IN)
		{
			float edges = 1 - dot(IN.viewDir, IN.worldNormal);

			return edges;
		}

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 color = _MainColor;

			float fresnelOutput = CalculateEdges(IN);
			float fresnelEmission = smoothstep(_SmoothnessEmission, 1 - _SmoothnessEmission, fresnelOutput + _EmissionFactor);

			o.Albedo = color.rgb;
			o.Emission = fresnelEmission * _EdgesColor;
			o.Alpha = color.a * _MeshAlpha;
			//o.Alpha = o.Alpha * (((1 - _MaskTexColor) + (_MaskTexColor)).a * _MaskTexAlpha);






            //// Albedo comes from a texture tinted by color
            //fixed4 c = energyColor * _Color;
            //o.Albedo = c.rgb;

            //// Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            //o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
