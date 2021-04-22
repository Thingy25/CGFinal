Shader "Unlit/ForceField"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _ForcefieldTexture("Force Field Texture", 2D) = "white" {}
        _RingDistorsion("Forcefield Noise", 2D) = "white" {}
        _RingDistorsionTransforms("Ring Distorsion Transforms (Speed/Scale)", Vector) = (0,0,1,1)
        _RingDistorsionMagnitude("Ring Distorsion Mag", Float) = 0.125
        _RingDistorsionSpeed("Ring Distorsion Speed", Float) = 0
        _FresnelIntensity("Fresnel Intensity", Float) = 1
        _FresnelDisplacement("Fresnel Displacement", Range(-1, 1)) = 1
        _FresnelFade("Fresnel Fade", Range(0, 0.49)) = 0.5
        _FresnelRingIntensity("Fresnel Ring Intensity", Float) = 1
        _FresnelRingDisplacement("Fresnel Ring Displacement", Range(-1, 1)) = 1
        _FresnelRingFade("Fresnel Ring Fade", Range(0, 0.49)) = 0.5

        [HDR]_PrimaryColor("Primary Color", Color) = (1,1,1,1)
        [HDR]_SecondaryColor("Secondary Color", Color) = (1,1,1,1)

        [Space][Header(Edge)][Space]
        _EdgeStrength("Edge Strength", Float) = 1
        _EdgeIntensity("Edge Intensity", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        //BlendOp Max
        ZWrite Off

        Pass
        {
            Tags {"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Assets/Packables/Shaders/Includes/MathLibrary.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 localVertex : TEXCOORD1;
                float3 localNormalSpace : TEXCOORD2;
                half4 fresnelValue : TEXCOORD3;
                half4 scrPos : TEXCOORD4;
            };

            uniform sampler2D _CameraDepthTexture; //Depth Texture

            sampler2D _MainTex;
            sampler2D _ForcefieldTexture;
            sampler2D _RingDistorsion;
            float4 _MainTex_ST;
            half4 _RingDistorsionTransforms;
            half4 _PrimaryColor;
            half4 _SecondaryColor;
            half _RingDistorsionMagnitude;
            half _RingDistorsionSpeed;
            half _FresnelIntensity;
            half _FresnelDisplacement;
            half _FresnelFade;
            half _FresnelRingIntensity;
            half _FresnelRingDisplacement;
            half _FresnelRingFade;

            half _EdgeStrength;
            half _EdgeIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.localVertex = v.vertex;
                o.localNormalSpace = v.normal;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.fresnelValue = Fresnel(normalize(ObjSpaceViewDir(v.vertex)), mul(unity_ObjectToWorld, v.normal), 1, 1);
                o.scrPos = ComputeScreenPos(o.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                //Noise Pass
                half4 col = tex2D(_MainTex, i.uv);
                half3 absNormalLocalSpace = abs(i.localNormalSpace) / 1.5;
                
                half2 ringNoiseUVsXY = tex2D(_RingDistorsion, i.localVertex.xy * _RingDistorsionTransforms.zw - _Time.x * _RingDistorsionTransforms.xy).xy * absNormalLocalSpace.z;
                half2 ringNoiseUVsXZ = tex2D(_RingDistorsion, i.localVertex.xz * _RingDistorsionTransforms.zw - _Time.x * _RingDistorsionTransforms.xy).xy * absNormalLocalSpace.y;
                half2 ringNoiseUVsYZ = tex2D(_RingDistorsion, i.localVertex.yz * _RingDistorsionTransforms.zw - _Time.x * _RingDistorsionTransforms.yx).xy * absNormalLocalSpace.x;
                
                half2 outputUVs = saturate(ringNoiseUVsXY + ringNoiseUVsXZ + ringNoiseUVsYZ) * _RingDistorsionMagnitude;

                half4 ringNoise = tex2D(_ForcefieldTexture, outputUVs - (_Time.x * _RingDistorsionSpeed));
                ringNoise *= smoothstep(_FresnelFade, 1 - _FresnelFade, i.fresnelValue + _FresnelDisplacement) * _FresnelIntensity;
                ringNoise += smoothstep(_FresnelRingFade, 1 - _FresnelRingFade, i.fresnelValue + _FresnelRingDisplacement) * _FresnelRingIntensity;

                //Depth Pass
                half depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos))); // depth
                half4 edges = (_EdgeStrength * (depth - i.scrPos.w));
                edges = saturate(edges); // clamp to prevent weird artifacts
                edges = 1 - edges;
                edges *= _EdgeIntensity;

                half4 output = saturate(ringNoise + edges);
                output.rgb = lerp(_SecondaryColor, _PrimaryColor.rgb, output.rgb);

                return output;
            }
            ENDCG
        }
    }
}
