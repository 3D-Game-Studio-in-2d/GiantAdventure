#ifndef MY_TOON_SHADER_INCLUDE
#define MY_TOON_SHADER_INCLUDE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"

CBUFFER_START(UnityPerMaterial)
    TEXTURE2D(_ColorMap);
    SAMPLER(sampler_ColorMap);
    float4 _ColorMap_ST;
    float4 _Color;
CBUFFER_END

struct Attributes
{
    float4 positionOS : POSITION;
    float3 normalOS   : NORMAL;
    float2 uv         : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float4 positionHCS : SV_POSITION;
    float2 uv          : TEXCOORD0;
    float3 positionWS  : TEXCOORD1;
    float3 normalWS    : TEXCOORD2;
    UNITY_VERTEX_INPUT_INSTANCE_ID
    UNITY_VERTEX_OUTPUT_STEREO
};

Varyings Vertex(Attributes IN)
{
    Varyings OUT;
    UNITY_SETUP_INSTANCE_ID(IN);
    UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

    OUT.positionWS = mul(unity_ObjectToWorld, IN.positionOS).xyz;
    OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
    OUT.positionHCS = TransformWorldToHClip(OUT.positionWS);
    OUT.uv = TRANSFORM_TEX(IN.uv, _ColorMap);
    return OUT;
}

float FragmentDepthOnly(Varyings IN) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(IN);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
    return 0;
}

float4 FragmentDepthNormalsOnly(Varyings IN) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(IN);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
    return float4(normalize(IN.normalWS), 0);
}

float4 Fragment(Varyings IN) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(IN);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

    // Получаем данные света
    Light light = GetMainLight();
    float3 normal = normalize(IN.normalWS);
    float NdotL = dot(normal, light.direction);
    
    // Toon step — дискретное освещение
    float lightLevel = step(0.5, NdotL); // только 0 или 1
    
    // Чтение цвета из текстуры
    float4 texColor = SAMPLE_TEXTURE2D(_ColorMap, sampler_ColorMap, IN.uv);
    float4 finalColor = texColor * _Color;

    // Применение света
    finalColor.rgb *= light.color * lightLevel;

    return finalColor;
}

#endif
