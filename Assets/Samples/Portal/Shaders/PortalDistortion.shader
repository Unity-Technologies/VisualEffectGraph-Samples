Shader "Unlit/PortalDistortion"
{
    Properties
    {
        _Radius("Radius", Float) = 1.0
        _Thickness("Thickness", Float) = 0.7
        _DisplacementAngle("DisplacementAngle", Float) = -0.5
        _Intensity("Intensity", Float) = 1
        _NoiseIntensity("NoiseIntensity", Float) = 0.0
        _NoiseTime("NoiseTime", Float) = 0.0
        _NoiseTexture("NoiseTexture", 2D) = "" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Transparent-1"  }
        LOD 100
        Cull Off
        ZWrite Off

        Pass
        {
            Tags{ "LightMode" = "ForwardOnly" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"

            float _Radius;
            float _Thickness;
            float _Intensity;
            float _DisplacementAngle;

            float _NoiseIntensity;
            float _NoiseTime;
            sampler2D _NoiseTexture;

            struct appdata
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
				o.pos = TransformWorldToHClip(TransformObjectToWorld(v.pos));
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv * 2.0f - 1.0f;
                float power = 0.0f;
                float dist = length(uv) - _Radius;
                if (dist < 0.0f)
                    power = saturate(1.0f + dist / _Thickness);

                power *= power;
                power *= saturate((i.pos.w - 0.3) * 3.0f); // near plane fade out (values hardcoded!)
                power *= _Intensity;

                float noise = tex2D(_NoiseTexture,float2(dist, _NoiseTime)).x * 2.0f - 1.0f;

                float dAngle = power * _DisplacementAngle * (1.0f - noise * _NoiseIntensity);
                float dSin = sin(dAngle);
                float dCos = cos(dAngle);

                float2 duv = (float2(uv.x * dCos - uv.y * dSin, uv.y * dCos + uv.x * dSin) - uv);
                float2 dUVtoScreen = float2(1.0f / ddx(uv.x), 1.0f / ddy(uv.y)); // remap on screen using derivatives (incorrect but ok)
                float2 grabPos = i.pos.xy + duv * dUVtoScreen;
                grabPos = saturate(grabPos / _ScreenParams.xy);

                //float4 bgcolor = SAMPLE_TEXTURE2D(_ColorPyramidTexture, sampler_ColorPyramidTexture, _ColorPyramidScale.xy * grabPos);
				float4 bgcolor = SAMPLE_TEXTURE2D_X(_ColorPyramidTexture, s_trilinear_clamp_sampler, grabPos * _ColorPyramidScale.xy);
                return float4(bgcolor.rgb,1);
            }
            ENDHLSL
        }
    }
}
