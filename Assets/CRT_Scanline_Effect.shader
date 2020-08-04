Shader "Botaemic/Unlit/CRT_Scanline_Effect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AmountOfScanlines("Amount of Scanlines", int) = 800
        _ScanlineOpacity("Opacity", range (0.01, 1) ) = 0.04
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
            int _AmountOfScanlines;
            float _ScanlineOpacity;

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
                // distance from center of image
                float2 uv = i.uv;
                float d = length(uv - float2(0.5, 0.5));

                // final color
                float3 col;
                col.r = tex2D(_MainTex, float2(uv.x, uv.y)).r;
                col.g = tex2D(_MainTex, uv).g;
                col.b = tex2D(_MainTex, float2(uv.x, uv.y)).b;

                // scanline
                float scanline = sin(uv.y * _AmountOfScanlines) * _ScanlineOpacity;
                col -= scanline;

                return float4(col, 1.0);
            }
            ENDCG
        }
    }
}
