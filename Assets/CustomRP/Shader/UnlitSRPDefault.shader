Shader "Custom/SimpleSRPShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        Pass
        {
            Tags
            {
                "LightMode"="SRPDefaultUnlit"
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            float4 _Color;
            float4x4 UNITY_MATRIX_MVP;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = mul(UNITY_MATRIX_MVP, input.positionOS);
                return output;
            }

            float4 frag(Varyings input) : SV_Target
            {
                return _Color;
            }
            ENDHLSL
        }
    }
}