#ifndef LIGHTING_CORE_G1 //Preprocessor directive
#define LIGHTING_CORE_G1



//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl" // (final inferido)

void GetMainLight_float(float3 positionWS, out float3 directionWS, out float3 color, out float shadowAttenuation) // (final de la firma inferido)
{
#if defined(SHADERGRAPH_PREVIEW)
    directionWS = float3(1,1,-1);
    color = 1;
    shadowAttenuation = 1;
#else
    float4 shadowCoord = TransformWorldToShadowCoord(positionWS);
    Light mainLight = GetMainLight(shadowCoord);

    directionWS = mainLight.direction;
    color = mainLight.color;
    shadowAttenuation = mainLight.shadowAttenuation;
#endif
}

void SimpleLightingAdditional_float(float3 positionWS, float2 uvSS, float3 normalWS, float3 viewDirWS, float roughness, out float3 lit) // (final de la firma inferido)
{
#if defined(SHADERGRAPH_PREVIEW)
    lit = 0;
#else
    uint additionalLightCount = GetAdditionalLightsCount();

#ifdef USE_CLUSTER_LIGHT_LOOP
    InputData inputData = (InputData)0;
    inputData.normalizedScreenSpaceUV = uvSS;
    inputData.normalWS = normalWS;
    inputData.positionWS = positionWS;
#endif


    LIGHT_LOOP_BEGIN(additionalLightCount)

    Light light = GetAdditionalLight(lightIndex, positionWS, 1);

    float lambert = max(dot(normalWS, light.direction), 0);
    float3 halfwayVector = normalize(light.direction + viewDirWS);
    float blinnPhong = max(dot(halfwayVector, normalWS), 0);

    float3 diffuse = lambert * light.distanceAttenuation * light.color;
    float3 specular = blinnPhong * light.distanceAttenuation * light.color;

    lit += (diffuse + specular) * light.shadowAttenuation;

    LIGHT_LOOP_END

#endif
}

void BlinnPhongAdditional_float(float2 uvSS, float3 normalWS, float3 viewDirWS, float3 positionWS, float roughness, out float3 lit)
{
#if defined(SHADERGRAPH_PREVIEW)
    lit = 0;
#else
    uint additionalLightCount = GetAdditionalLightsCount();

#ifdef USE_CLUSTER_LIGHT_LOOP
    InputData inputData = (InputData)0;
    inputData.normalizedScreenSpaceUV = uvSS;
    inputData.normalWS = normalWS;
    inputData.positionWS = positionWS;
#endif


    LIGHT_LOOP_BEGIN(additionalLightCount)

    Light light = GetAdditionalLight(lightIndex, positionWS, 1);

    
    float3 halfwayVector = normalize(light.direction + viewDirWS);
    float blinnPhong = max(dot(halfwayVector, normalWS), 0);

   
    float3 specular = pow(blinnPhong,500) * light.distanceAttenuation * light.color;

    lit += specular * light.shadowAttenuation;

    LIGHT_LOOP_END

#endif
}

#endif