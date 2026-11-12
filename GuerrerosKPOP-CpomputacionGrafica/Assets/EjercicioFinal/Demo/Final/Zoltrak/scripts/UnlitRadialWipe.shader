Shader "Universal Render Pipeline/Custom/Unlit Angular Wipe"
{
    Properties
    {
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}
        [MainColor] _BaseColor("Color", Color) = (1,1,1,1)
        [HDR] _EmissionColor("Emission Color", Color) = (0,0,0)
        
        _Progress("Progress", Range(0.0, 1.0)) = 0.0
        _EdgeSoftness("Edge Softness", Range(0.001, 0.5)) = 0.1
        _DimIntensity("Dim Intensity (Apagado)", Range(0.0, 1.0)) = 0.65

        [HideInInspector] _Surface("__surface", Float) = 1.0
        [HideInInspector] _Blend("__blend", Float) = 0.0
        [HideInInspector] _AlphaClip("__clip", Float) = 0.0
        [HideInInspector] _SrcBlend("__src", Float) = 5.0
        [HideInInspector] _DstBlend("__dst", Float) = 10.0
        [HideInInspector] _ZWrite("__zw", Float) = 0.0
        [HideInInspector] _Cull("__cull", Float) = 2.0
    }

    SubShader
    {
        Tags 
        { 
            "RenderType" = "Transparent" 
            "Queue" = "Transparent" 
            "RenderPipeline" = "UniversalPipeline" 
        }

        Blend [_SrcBlend] [_DstBlend]
        ZWrite [_ZWrite]
        Cull [_Cull]

        Pass
        {
            Name "UnlitAngularWipe"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
                float4 color        : COLOR;
            };

            struct Varyings
            {
                float4 positionCS   : SV_POSITION;
                float2 uv           : TEXCOORD0;
                float4 color        : COLOR;
            };

            Texture2D _BaseMap;
            SamplerState sampler_BaseMap;

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                float4 _BaseColor;
                float4 _EmissionColor;
                float _Progress;
                float _EdgeSoftness;
                float _DimIntensity;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
                output.color = input.color;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                half4 texColor = _BaseMap.Sample(sampler_BaseMap, input.uv) * _BaseColor;

                // Coordenadas polares angulares
                float2 centeredUV = input.uv - 0.5f;
                float angle = atan2(centeredUV.y, centeredUV.x);
                float normalizedAngle = angle * 0.159154941f + 0.5f;

                // Máscara principal del barrido
                float sweep = smoothstep(_Progress - _EdgeSoftness, _Progress, normalizedAngle);
                
                // Intensidad base: estado "apagado" fuerte según _DimIntensity
                float intensity = lerp(_DimIntensity, 1.0f, sweep);

                // Cabecera brillante en el frente de onda
                float headGlow = smoothstep(_Progress - 0.03f, _Progress, normalizedAngle) * 
                                 (1.0f - smoothstep(_Progress, _Progress + 0.02f, normalizedAngle));

                // Color final con brillo HDR en el frente
                half3 finalColor = (texColor.rgb * _BaseColor.rgb * intensity) + (_EmissionColor.rgb * (sweep + headGlow * 3.0f));
                
                // Piso mínimo de alfa para mantener el cuerpo visible antes del paso del haz
                float alphaMask = max(intensity, headGlow * 2.0f);
                alphaMask = max(alphaMask, _DimIntensity); 

                half finalAlpha = texColor.a * alphaMask * input.color.a;

                return half4(finalAlpha > 0.01f ? finalColor : 0, finalAlpha);
            }
            ENDHLSL
        }
    }
    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}