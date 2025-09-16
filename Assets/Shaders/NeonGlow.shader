Shader "Custom/NeonRimGlowAnimated"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.2, 0.2, 0.2, 1)
        _RimPower ("Rim Power", Range(0.5, 8.0)) = 3.0
        _GlowIntensity ("Glow Intensity", Range(0, 5)) = 1.5
        _ColorSpeed ("Color Cycle Speed", Range(0.1, 5.0)) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 200

        Pass
        {
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS : NORMAL;
                float3 viewDirWS : TEXCOORD0;
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float _RimPower;
                float _GlowIntensity;
                float _ColorSpeed;
            CBUFFER_END

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.normalWS = normalize(TransformObjectToWorldNormal(IN.normalOS));
                float3 worldPos = TransformObjectToWorld(IN.positionOS).xyz;
                OUT.viewDirWS = normalize(GetCameraPositionWS() - worldPos);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                // Animated rim glow color (HSV cycling)
                float t = _Time.y * _ColorSpeed;
                float3 rimColor = 0.5 + 0.5 * float3(
                    sin(t),
                    sin(t + 2.094),   // offset by 120°
                    sin(t + 4.188)    // offset by 240°
                );

                // Rim lighting factor
                float rim = 1.0 - saturate(dot(IN.viewDirWS, IN.normalWS));
                rim = pow(rim, _RimPower);

                float3 finalColor = _BaseColor.rgb + rimColor * rim * _GlowIntensity;

                return half4(finalColor, 1.0);
            }
            ENDHLSL
        }
    }
}
