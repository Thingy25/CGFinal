Shader "Custom/Shield"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture", 2D) = "white" {}

        [HDR] _MainTexColor ("Main Texture Color", Color) = (1,1,1,1)
        [HDR] _MaskTexColor ("Mask Texture Color", Color) = (1,1,1,1)
		[HDR] _EdgesColor("Edges Color", Color) = (1,1,1,1)

		_SpeedX("Speed in X", Range(0,1)) = 0.1
		_SpeedY("Speed in Y", Range(0,1)) = 0.1

		_EmissionFactor("Factor Emission Slider", Range(-1,1)) = 0
		_SmoothnessEmission("Emission Smoothness", Range(0,0.5)) = 0

		_TilingSize("Triplanar Texture Tiling Size", Range(0,1)) = 0

        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
			float3 worldNormal;
			float3 worldPos;
			float3 viewDir;
        };
        
		sampler2D _MainTex;
		sampler2D _MaskTex;
		
		fixed4 _MainTexColor;
		fixed4 _MaskTexColor;
		fixed4 _EdgesColor;

		float _SpeedX;
		float _SpeedY;

		float _SmoothnessEmission;
		float _EmissionFactor;

		float _TilingSize;

        half _Glossiness;
        half _Metallic;
        
		inline float2 MoveTextures(float2 uvs, float speedx, float speedy)
		{
			float2 movingUVs = uvs;
			float distanceX = speedx.x * _Time.y;
			float distanceY = speedy.x * _Time.y;

			movingUVs += float2(distanceX, distanceY);

			return movingUVs;
		}

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
			float3 mainTex = tex2D(_MainTex, IN.uv_MainTex);

			fixed4 tb = tex2D(_MaskTex, MoveTextures(IN.worldPos.xz, _SpeedX, _SpeedY) * _TilingSize);
			fixed4 lr = tex2D(_MaskTex, MoveTextures(IN.worldPos.yz, _SpeedX, _SpeedY) * _TilingSize);
			fixed4 fb = tex2D(_MaskTex, MoveTextures(IN.worldPos.xy, _SpeedX, -_SpeedY) * _TilingSize);

			fixed4 tb_Color = IN.worldNormal.y * tb;
			fixed4 lr_Color = IN.worldNormal.x * lr;
			fixed4 fb_Color = IN.worldNormal.z * fb;

			fixed4 energyColor = (abs(tb_Color) + abs(lr_Color) + abs(fb_Color));

			float fresnelOutput = CalculateEdges(IN);
			float fresnelEmission = smoothstep(_SmoothnessEmission, 1 - _SmoothnessEmission, fresnelOutput + _EmissionFactor);

			o.Albedo = (mainTex * _MainTexColor) * (1 - energyColor) + (_MaskTexColor * energyColor);
			o.Emission = fresnelEmission * _EdgesColor;






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
