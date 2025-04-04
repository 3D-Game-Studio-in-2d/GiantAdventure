Shader "Custom/URP_GrayscaleLitSoftWithShadows"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _MainColor("Main Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float4 shadowCoord : TEXCOORD2;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainColor;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                float3 worldPos = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.uv = IN.uv;
                OUT.positionHCS = TransformWorldToHClip(worldPos);
                OUT.shadowCoord = TransformWorldToShadowCoord(worldPos);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                Light light = GetMainLight();
                float3 normal = normalize(IN.normalWS);
                float3 lightDir = normalize(light.direction);

                // Основное освещение
                float NdotL = saturate(dot(normal, lightDir));
                float softLight = pow(smoothstep(0.0, 1.0, NdotL), 1.2);

                // Тени
                float shadowAtten = MainLightRealtimeShadow(IN.shadowCoord);
                float lighting = softLight * shadowAtten;

                // Градации серого
                float grayscale = lighting;

                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * _MainColor;
                float3 finalColor = texColor.rgb * grayscale;

                return float4(finalColor, texColor.a);
            }

            ENDHLSL
        }
    }

    FallBack Off
}
