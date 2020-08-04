Shader "Botaemic/Unlit/Vignette_Effect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VignetteSize ("Size of Vignette", range (0 , 10)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _VignetteSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float d = length(uv - float2(0.5, 0.5));

                float3 col;
                col.r = tex2D(_MainTex, float2(uv.x, uv.y)).r;
                col.g = tex2D(_MainTex, uv).g;
                col.b = tex2D(_MainTex, float2(uv.x, uv.y)).b;

                // vignette
                col *= 1.0 - d * _VignetteSize;

                return float4(col, 1.0);
            }
            ENDCG
        }
    }
}
