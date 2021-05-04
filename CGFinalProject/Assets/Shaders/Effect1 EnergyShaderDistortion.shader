Shader "Custom/Shield/Energy/Distortion"
{
    Properties
    {
        _MaskTex ("Mask Texture", 2D) = "white" {}

        [HDR] _MainTexColor ("Main Texture Color", Color) = (1,1,1,1)
        [HDR] _MaskTexColor ("Mask Texture Color", Color) = (1,1,1,1)

		_TilingSize("Tiling Size", Range(0,0.5)) = 0
		_TimeNumber("Time", Range(0,1)) = 0

		_MainTexAlpha("Main Texture Alpha", Range(0,1)) = 0
		_MaskTexAlpha("Mask Texture Alpha", Range(0,1)) = 0

		_RingDistorsionTransforms("Ring Distorsion Transforms (Speed/Scale)", Vector) = (0,1,1,1)
		_RingDistorsionSpeed("Ring Distorsion Speed", Float) = 0
    }
    SubShader
    {
        Tags { 
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "TransparentCutout" 
		}
        //LOD 200
		Cull Off //Para que se vea por ambos lados (Comentado por ahora por situaciones técnicas).

		/*pass {
			ZWrite On
			ColorMask 0
		}*/

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha : fade vertex : vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
			float3 worldNormal;
			float3 worldPos;
			float3 viewDir;
        };
        
		sampler2D _MaskTex;
		
		fixed4 _MainTexColor;
		fixed4 _MaskTexColor;

		float _TilingSize;
		float _TimeNumber;

		float _MainTexAlpha;
		float _MaskTexAlpha;

		half4 _RingDistorsionTransforms;
		half _RingDistorsionSpeed;
        
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

			half2 tb = tex2D(_MaskTex, IN.worldPos.xz * _RingDistorsionTransforms.zw - (_Time.y * _TimeNumber) * _RingDistorsionTransforms.xz).xy * abs(IN.worldNormal.y);
			half2 lr = tex2D(_MaskTex, IN.worldPos.yz * _RingDistorsionTransforms.zw - (_Time.y * _TimeNumber) * _RingDistorsionTransforms.yz).xy * abs(IN.worldNormal.x);
			half2 fb = tex2D(_MaskTex, IN.worldPos.xy * _RingDistorsionTransforms.zw - (_Time.y * _TimeNumber) * _RingDistorsionTransforms.xy).xy * abs(IN.worldNormal.z);

			//Mirar como sacar el localvertexposition en el primer worldpos y en el segundo mirar el normallocalspace



			half2 outputUVs = saturate(tb + lr + fb);


			//fixed4 mask = tex2D(_MaskTex, outputUVs - (_Time.x * _RingDistorsionSpeed));
			fixed4 mask = tex2D(_MaskTex, IN.uv_MainTex + outputUVs * _TilingSize);

			//fixed4 lr = tex2D(_MaskTex, MoveTextures(IN.worldPos.yz, _SpeedX, _SpeedY) * _TilingSize);
			//fixed4 fb = tex2D(_MaskTex, MoveTextures(IN.worldPos.xy, _SpeedX, -_SpeedY) * _TilingSize);

			//fixed4 tb_Color = IN.worldNormal.y * tb;
			//fixed4 lr_Color = IN.worldNormal.x * lr;
			//fixed4 fb_Color = IN.worldNormal.z * fb;
/*
			fixed4 energyColor = (abs(tb_Color) + abs(lr_Color) + abs(fb_Color));

			float fresnelOutput = CalculateEdges(IN);
			float fresnelEmission = smoothstep(_SmoothnessEmission, 1 - _SmoothnessEmission, fresnelOutput + _EmissionFactor);*/

			//o.Albedo = mask;

			o.Albedo = _MainTexColor * (1 - mask) + (_MaskTexColor * mask);
			//o.Emission = fresnelEmission * _EdgesColor;

			float alpha = _MainTexColor.a * _MainTexAlpha + (mask.rgb * _MaskTexAlpha * 2);

			o.Alpha =  saturate(alpha);
        }

		void vert(inout appdata_full v, out Input IN) {
			UNITY_INITIALIZE_OUTPUT(Input, IN);

			float x = v.vertex.x;
			float yOriginal = v.vertex.y;
			float z = v.vertex.z;

			float4 mask = tex2Dlod(_MaskTex, float4(v.texcoord.xy, 0, 0));

			float yModificado = sin(2 * x);

			float y = lerp(yOriginal, yModificado, mask.r);

			v.vertex.xyz += float3(x, yModificado, z);
			v.normal = normalize(float3(v.normal.x, v.normal.y, v.normal.z));
		}
        ENDCG
    }
    FallBack "Diffuse"
}
