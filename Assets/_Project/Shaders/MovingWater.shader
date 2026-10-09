Shader "Playable/MovingWater"
{
    Properties { _Color ("Water tint", Color) = (0.58,0.77,0.85,1) }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Background" }
        Pass
        {
            Cull Off
            ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            struct appdata { float4 vertex : POSITION; };
            struct v2f { float4 position : SV_POSITION; fixed4 color : COLOR; };
            fixed4 _Color;
            v2f vert(appdata v)
            {
                v2f o;
                o.position = float4(v.vertex.xy, 0.99, 1);
                float drift = _Time.y * 0.4;
                float wave = sin(v.vertex.y * 11 + drift + sin(v.vertex.x * 6 - drift) * 1.4);
                float light = smoothstep(0.5, 1, wave) * 0.075;
                o.color = fixed4(_Color.rgb * (0.94 + wave * 0.12) + light, 1);
                return o;
            }
            fixed4 frag(v2f i) : SV_Target { return i.color; }
            ENDCG
        }
    }
}
